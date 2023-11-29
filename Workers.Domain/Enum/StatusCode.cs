namespace Workers.Domain.Enum;

public enum StatusCode
{
    UserNotFound = 0,
    UserAlreadyExists = 1,
        
    CarNotFound = 10,

    OrderNotFound = 20,

    Ok = 200,
    InternalServerError = 500
}