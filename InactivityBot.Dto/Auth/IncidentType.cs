using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace InactivityBot.Dto.Auth
{
    public enum IncidentType
    {
        Unknown,
        InvalidCredentials,
        ExpiredToken,
        InvalidToken,
        Login,
        LockedOut,
        AttemptToLogInAsLockedOutUser
    }
}