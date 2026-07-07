namespace P1.Application.Contracts.Common;

public class Result
{
    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected Result(bool isSuccess, string error, T? value) : base(isSuccess, error)
    {
        _value = value;
    }

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("دسترسی به مقدار نتیجه ناموفق امکان‌پذیر نیست.");

    public static Result<T> Success(T value) => new(true, string.Empty, value);
    public new static Result<T> Failure(string error) => new(false, error, default);
}
