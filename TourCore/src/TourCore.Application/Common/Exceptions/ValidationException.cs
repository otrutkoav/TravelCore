using System;
using System.Collections.Generic;
using System.Linq;

namespace TourCore.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
            Errors = new string[0];
        }

        public ValidationException(IEnumerable<string> errors)
            : base("One or more validation errors occurred.")
        {
            Errors = errors == null ? new string[0] : errors.ToArray();
        }

        public IReadOnlyCollection<string> Errors { get; private set; }
    }
}