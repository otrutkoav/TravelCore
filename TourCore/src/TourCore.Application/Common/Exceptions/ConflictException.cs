using TourCore.Application.Common.Errors;

namespace TourCore.Application.Common.Exceptions
{
    public class ConflictException : ApplicationExceptionBase
    {
        public ConflictException(string message)
            : base(message, ErrorCode.Conflict)
        {
        }

        public ConflictException(string message, string code)
            : base(message, code)
        {
        }
    }
}