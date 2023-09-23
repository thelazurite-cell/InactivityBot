using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using InactivityBot.Dto.Auth;
using InactivityBot.Dto.Interfaces;
using static InactivityBot.Dto.Filtering.MongoQueryHelper;

namespace InactivityBot.Dto.Filtering
{
    public class MongoFilterParser : IParser
    {
        public string Result { get; set; } = string.Empty;
        public RequestReport Report { get; } = new();


        public static bool QueryHasValidStructure(QueryGroup parameters)
        {
            return parameters != null && parameters.Queries != null && parameters.Queries.Count > 0;
        }

        public bool Parse(Type transferObjectType, IComparableItem? comparable)
        {
            if (EnsureValueNotNull(comparable)) return true;
            return comparable switch
            {
                QueryGroup queryGroup => GenerateForQueryGroup(transferObjectType, queryGroup),
                Query query => GenerateForQuery(transferObjectType, query),
                _ => UnexpectedDataProvided()
            };
        }

        private bool EnsureValueNotNull(IComparableItem? comparable)
        {
            if (comparable != null) return false;
            Result = "{}";
            Report.Messages.Add(new Message
            {
                ErrorCode = ApiErrorCode.NoQueryProvided,
                Level = IncidentLevel.Warning,
                MessageText = "No filter was provided, finding all instead"
            });
            return true;
        }

        private bool UnexpectedDataProvided()
        {
            AddError(ApiErrorCode.InvalidDataType, "Unexpected comparable item provided");
            return false;
        }

        private bool GenerateForQuery(Type transferObjectType, Query query)
        {
            if (!AttemptToFindProperty(transferObjectType, query, out var isString)) return false;
            return GenerateQueryType(query, isString);
        }

        private bool GenerateQueryType(Query query, bool requiresQuoteWrap)
        {
            if (query.ComparisonType == null)
            {
                AddError(ApiErrorCode.QueryRequiresComparisonType, "A filter must provide a comparison type");
                return false;
            }

            Result += "{ ";
            Result += $"{query.FieldName}: ";
            Result += "{ ";
            Result += $"{(query.ComparisonType ?? FilterType.None).ToComparisonType()}:  ";
            if (query.FieldValue.Count > 1 || query.ComparisonType == FilterType.In)
            {
                Result += "[";

                Result += string.Join(",", requiresQuoteWrap ? ToQuotedValueArray(query) : query.FieldValue);
                Result += "]";
            }
            else
            {
                Result += requiresQuoteWrap ? $"\"{query.FieldValue[0]}\"" : query.FieldValue[0];
            }

            Result += " }";
            Result += " }";
            return true;
        }

        private static List<string> ToQuotedValueArray(Query query)
        {
            var tmp = query.FieldValue.Select(value => $"\"{value}\"").ToList();
            return tmp;
        }

        private bool AttemptToFindProperty(Type transferObjectType, Query query, out bool requiresQuoteWrap)
        {
            requiresQuoteWrap = true;
            var found = false;
            var properties = transferObjectType.GetProperties();
            foreach (var property in properties)
            {
                if (!FindProperty(query, ref requiresQuoteWrap, property, ref found)) return false;
            }

            if (!found)
            {
                AddError(ApiErrorCode.InvalidProperty, $"{query.FieldName} is not a valid property.");
                return false;
            }

            return true;
        }

        private bool FindProperty(Query query, ref bool requiresQuoteWrap, PropertyInfo property, ref bool found)
        {
            foreach (var attribute in property.GetCustomAttributes())
            {
                if (attribute is not JsonPropertyNameAttribute jsonAttrib) continue;
                if (jsonAttrib.Name != query.FieldName) continue;
                found = true;
                requiresQuoteWrap = RequiresQuoteWrap(property);
                if (!PerformBooleanValidationIfRequired(query, property))
                    return false;
            }

            return true;
        }

        private bool PerformBooleanValidationIfRequired(Query query, PropertyInfo property)
        {
            if (property.PropertyType != typeof(bool)) return true;
            if (query.FieldValue[0].ToLower() == "true" || query.FieldValue[0].ToLower() == "false")
                return true;
            AddError(ApiErrorCode.InvalidPropertyValue, "Value must be true or false for a boolean",
                new List<string> { query.FieldName, string.Join(",", query.FieldValue) });
            return false;
        }

        private bool GenerateForQueryGroup(Type transferObjectType, QueryGroup queryGroup)
        {
            switch (queryGroup.ComparisonType)
            {
                case FilterType.ById:
                    return GenerateByIdQuery(queryGroup);
                case FilterType.And:
                case FilterType.Or:
                    return GenerateGroupedQuery(transferObjectType, queryGroup);
                case FilterType.Set:
                    return GenerateAttemptInvalid(queryGroup);
                default:
                    return GenerateQuerySection(transferObjectType, queryGroup);
            }
        }

        private bool GenerateAttemptInvalid(QueryGroup queryGroup)
        {
            Report.Messages.Add(new Message
            {
                ErrorCode = ApiErrorCode.InvalidQueryStructure,
                Level = IncidentLevel.Error,
                MessageText = "Cannot attempt to set a value during a filter",
                Parameters = { queryGroup.TypeDiscriminator }
            });
            return false;
        }

        private bool GenerateGroupedQuery(Type transferObjectType, QueryGroup queryGroup)
        {
            if (!QueryHasValidStructure(queryGroup))
            {
                AddError(ApiErrorCode.InvalidQueryStructure, "Query structure is invalid.");
                return false;
            }

            if (queryGroup.Queries == null || queryGroup.Queries.Count != 2)
            {
                AddError(ApiErrorCode.NeedTwoParameters, "You need two parameters for and/or statements.");
                return false;
            }

            Result += $"\"${(queryGroup.ComparisonType ?? FilterType.Or).ToComparisonType()}\": [";
            var successful = GenerateQuerySection(transferObjectType, queryGroup);
            Result += "]";
            return successful;
        }

        private bool GenerateQuerySection(Type transferObjectType, QueryGroup queryGroup)
        {
            if (!QueryHasValidStructure(queryGroup))
            {
                AddError(ApiErrorCode.InvalidQueryStructure, "Query structure is invalid.");
                return false;
            }

            for (var enumerator = 0; enumerator < queryGroup.Queries.Count; enumerator++)
            {
                var result = Parse(transferObjectType, queryGroup.Queries[enumerator] as IComparableItem);
                if (enumerator + 1 != queryGroup.Queries.Count)
                {
                    Result += ",";
                }

                if (!result) return false;
            }

            return true;
        }

        private bool GenerateByIdQuery(QueryGroup queryGroup)
        {
            if (queryGroup.Queries == null || queryGroup.Queries.Count != 1)
            {
                AddError(ApiErrorCode.NeedOneParametersOnly, "You need one parameter to find by id");
                return false;
            }

            Result =
                $"{{{FilterType.ById.ToComparisonType()}: ObjectId(\"{((Query)queryGroup.Queries[0]).FieldValue[0]}\")}}";
            return true;
        }

        private void AddError(ApiErrorCode errorCode, string message, List<string> parameters = null)
        {
            Report.IsSuccess = false;
            Report.Messages.Add(new Message
            {
                ErrorCode = errorCode,
                MessageText = message,
                Level = IncidentLevel.Error,
                Parameters = parameters
            });
        }
    }
}