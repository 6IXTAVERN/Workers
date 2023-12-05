using Workers.Domain.Enum;
using Workers.Domain.Interfaces;

namespace Workers.Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
    
    public BaseResponse(string description, StatusCode statusCode, T data = default(T))
    {
        Description = description;
        StatusCode = statusCode;
        Data = data;
    }
}