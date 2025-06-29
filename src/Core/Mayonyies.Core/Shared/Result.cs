namespace Mayonyies.Core.Shared;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure => !IsSuccess;
}

public interface IResult<out TValue, out TError> : IResult
{
    TValue Value { get; }
    TError Error { get; }
}

public interface IResult<out TValue> : IResult<TValue, Error>;

public readonly struct Result : IResult<Success, Error>
{
    private Result(Success success)
    {
        IsSuccess = true;

        Value = success;
    }

    private Result(Error error)
    {
        IsSuccess = false;

        if (IsFailure)
            Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Success Value { get; }
    public Error Error { get; }

    public static implicit operator Result(Success success) => Success(success);
    public static implicit operator Result(Error error) => Failure(error);

    public static Result Success() =>
        new(Successes.General.Success());

    public static Result Success(Success success) =>
        new(success);

    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value);

    public static Result<TValue, TError> Success<TValue, TError>(TValue value) =>
        new(true, default!, value);

    public static Result Failure(Error error) =>
        new(error);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(error);

    public static Result<TValue, TError> Failure<TValue, TError>(TError error) =>
        new(false, error, default!);

    public void Deconstruct(out bool isSuccess, out bool isFailure, out Success value, out Error error)
    {
        isSuccess = IsSuccess;
        isFailure = IsFailure;
        value = Value;
        error = Error;
    }
}

public readonly struct Result<TValue> : IResult<TValue>
{
    public bool IsFailure => !IsSuccess;
    public bool IsSuccess { get; }
    public Error Error { get; }
    public TValue Value { get; }

    internal Result(TValue value)
    {
        Value = value;

        IsSuccess = true;
    }

    internal Result(Error error)
    {
        Error = error;
        Value = default!;

        IsSuccess = false;
    }

    internal Result(bool isSuccess, Error error, TValue value)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        if (value is not IResult<TValue> result)
            return Result.Success(value);

        var resultError = result.IsFailure ? result.Error : default;
        var resultValue = result.IsSuccess ? result.Value : default!;

        return new Result<TValue>(result.IsSuccess, resultError, resultValue);
    }

    public static implicit operator Result<TValue>(Error error) =>
        Result.Failure<TValue>(error);

    public static implicit operator Result(Result<TValue> result) =>
        result.IsSuccess
            ? Result.Success(result.Value)
            : Result.Failure(result.Error);

    public void Deconstruct(out bool isSuccess, out bool isFailure, out TValue value, out Error error)
    {
        isSuccess = IsSuccess;
        isFailure = IsFailure;
        value = Value;
        error = Error;
    }
}

public readonly struct Result<TValue, TError> : IResult<TValue, TError>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public TError Error { get; }
    public TValue Value { get; }

    internal Result(TValue value)
    {
        IsSuccess = true;

        Value = value;
        Error = default!;
    }

    internal Result(TError error)
    {
        IsSuccess = false;

        Error = error;
        Value = default!;
    }

    internal Result(bool isSuccess, TError error, TValue value)
    {
        IsSuccess = isSuccess;

        Error = error;
        Value = value;
    }

    public static implicit operator Result<TValue, TError>(TValue value)
    {
        if (value is not IResult<TValue, TError> result)
            return Result.Success<TValue, TError>(value);

        var resultError = result.IsFailure ? result.Error : default!;
        var resultValue = result.IsSuccess ? result.Value : default!;

        return new Result<TValue, TError>(result.IsSuccess, resultError, resultValue);
    }

    public static implicit operator Result<TValue, TError>(TError error)
    {
        if (error is not IResult<TValue, TError> result)
            return Result.Failure<TValue, TError>(error);

        var resultError = result.IsFailure ? result.Error : default!;
        var resultValue = result.IsSuccess ? result.Value : default!;

        return new Result<TValue, TError>(result.IsSuccess, resultError, resultValue);
    }

    public void Deconstruct(out bool isSuccess, out bool isFailure, out TValue value, out TError error)
    {
        isSuccess = IsSuccess;
        isFailure = IsFailure;
        error = Error;
        value = Value;
    }
}