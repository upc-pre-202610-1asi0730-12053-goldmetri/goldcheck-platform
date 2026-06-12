namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Errors;

public enum IamError
{
    None,
    UserNotFound,
    InvalidCredentials,
    UsernameAlreadyExists,
    EmailAlreadyRegistered,
    InvalidRole,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}