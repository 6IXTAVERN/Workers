using Workers.Domain.Enum;

namespace Workers.Domain.Interfaces;

public interface IBaseResponse<T>
{
    string Description { get; set; }
    T Data { get; set; }
    StatusCode StatusCode { get; set; }
}