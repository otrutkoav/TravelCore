using System;
using System.Collections.Generic;
using System.Linq;

namespace TourCore.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>
            {
                { "General", new[] { message } }
            };
        }

        public ValidationException(IEnumerable<string> errors)
            : base("Validation failed.")
        {
            Errors = new Dictionary<string, string[]>
            {
                { "General", errors?.ToArray() ?? new string[0] }
            };
        }

        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base("Validation failed.")
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}