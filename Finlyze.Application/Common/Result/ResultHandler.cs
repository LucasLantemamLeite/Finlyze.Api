namespace Finlyze.Application.Abstracts.Interfaces.Handlers.Result;

public class ResultHandler<T>
{
    public string? Message { get; }
    public bool Success { get; }
    public T? Data { get; }

    public ResultHandler(bool success, string? message = null, T? data = default)
    {
        Message = message;
        Success = success;
        Data = data;
    }

    public static ResultHandler<T> Ok(string? message, T? data) => new ResultHandler<T>(true, message, data);

    public static ResultHandler<T> Fail(string? message) => new ResultHandler<T>(false, message);
}