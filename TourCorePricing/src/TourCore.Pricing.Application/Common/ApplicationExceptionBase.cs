namespace TourCore.Pricing.Application.Common;

public abstract class ApplicationExceptionBase : Exception
{
    protected ApplicationExceptionBase(string message, string code)
        : base(message)
    {
        Code = code;
    }

    public string Code { get; }
}

public sealed class NotFoundException : ApplicationExceptionBase
{
    public NotFoundException(string message, string code)
        : base(message, code)
    {
    }
}

public sealed class ValidationException : ApplicationExceptionBase
{
    public ValidationException(string message)
        : this(new Dictionary<string, string[]> { ["General"] = new[] { message } })
    {
    }

    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base("Ошибка валидации.", "validation_error")
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
