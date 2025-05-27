namespace Spectrophotometer.Service;

public class LoadResult<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess => string.IsNullOrEmpty(Message);

    public static LoadResult<T> Seccess(T data) => new LoadResult<T> { Data = data };
    public static LoadResult<T> Failure(string message) => new LoadResult<T> { Message = message };
}
