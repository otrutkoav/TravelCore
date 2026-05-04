using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Common.Exceptions
{
    public class ValidationException : ApplicationExceptionBase
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationException(string message)
            : base(ErrorMessages.ValidationError, ErrorCode.ValidationError)
        {
            Errors = new Dictionary<string, string[]>
            {
                { "General", new[] { message } }
            };
        }

        public ValidationException(IEnumerable<string> errors)
            : base(ErrorMessages.ValidationError, ErrorCode.ValidationError)
        {
            Errors = new Dictionary<string, string[]>
            {
                { "General", errors?.ToArray() ?? new string[0] }
            };
        }

        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base(ErrorMessages.ValidationError, ErrorCode.ValidationError)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}