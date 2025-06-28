namespace ChronoLogic.Ui.Common;

public abstract class ResultBase
{
    public bool IsSuccess { get; protected set; }
    public string? ErrorMessage { get; protected set; }
}

public class Result : ResultBase
{
    private Result(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }
    
    public static Result Success() => new Result(true, null);
    public static Result Failure(string message) => new Result(false, message);
}

public class Result<TValue> : ResultBase where TValue : class
{
    public TValue? Value { get; }

    private Result(bool isSuccess, string? errorMessage, TValue? value)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Value = value;
    }

    public static Result<TValue> Success(TValue value) => new Result<TValue>(true, null, value);
    public static Result<TValue> Failure(string message) => new Result<TValue>(false, message, null);
}