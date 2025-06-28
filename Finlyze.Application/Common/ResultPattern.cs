namespace Finlyze.Application.Abstract.Interface.Result;

public class ResultPattern<T>
{
    public string? Message { get; }
    public bool Success { get; }
    public T? Data { get; }

    public ResultPattern(bool success, string? message = null, T? data = default)
    {
        Message = message;
        Success = success;
        Data = data;
    }

    public static ResultPattern<T> Ok(string? message, T? data) => new ResultPattern<T>(true, message, data);

    public static ResultPattern<T> Fail(string? message) => new ResultPattern<T>(false, message);
}