using TourCore.Application.Common.Errors;

namespace TourCore.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationExceptionBase
    {
        public NotFoundException(string message)
            : base(message, ErrorCode.NotFound)
        {
        }

        public NotFoundException(string message, string code)
            : base(message, code)
        {
        }
    }
}