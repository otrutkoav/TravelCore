namespace TourCore.Application.Common.Exceptions
{
    public class ConflictException : ApplicationExceptionBase
    {
        public ConflictException(string message)
            : base(message, "conflict")
        {
        }

        public ConflictException(string message, string code)
            : base(message, code)
        {
        }
    }
}