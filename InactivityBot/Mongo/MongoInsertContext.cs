using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MongoDB.Driver;
using Exception = System.Exception;
using InactivityBot.Dto;
using InactivityBot.Dto.Auth;

namespace InactivityBot.Mongo
{
    public static class MongoInsertContext
    {
        private const string InsertMethodName = "InsertOne";
        private const int InsertMethodParameterCount = 3;
        private const string ValidateInsertMethodName = "ValidateInsert";

        public static RequestReport PerformInsertRequest(object collection, string requestedType, object dto)
        {
            var insertMethod = collection?.GetType().GetMethods().FirstOrDefault(itm =>
                itm.Name == InsertMethodName && itm.GetParameters().Length == InsertMethodParameterCount);
            insertMethod.Invoke(collection,
                new[]
                {
                    dto, new InsertOneOptions() {BypassDocumentValidation = false}, default(CancellationToken)
                });
            return SuccessfulInsert(requestedType, dto);
        }

        private static RequestReport SuccessfulInsert(string requestedType, object dto)
        {
            return new()
            {
                IsSuccess = true,
                RowsAffected = 1,
                Results = new() { dto },
                Messages = new()
                {
                    new()
                    {
                        ErrorCode = ApiErrorCode.RequestCompleted,
                        Level = IncidentLevel.Information,
                        MessageText = "Item was inserted",
                        Parameters = new() {requestedType}
                    }
                }
            };
        }
    }
}