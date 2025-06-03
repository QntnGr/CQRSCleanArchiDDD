
namespace Domain.Errors;

public enum ErrorType
{
    Conflict = 0,
    NotFound = 1,
    BadRequest = 2,
    Validation = 3,
    Unexpected = 4,
}
