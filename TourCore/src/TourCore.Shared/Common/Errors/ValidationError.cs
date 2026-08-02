namespace TourCore.Application.Common.Exceptions
{
    public sealed class ValidationError
    {
        public ValidationError(string code, string message = null) { Code = code; Message = message; }
        public string Code { get; }
        public string Message { get; }
    }
}
