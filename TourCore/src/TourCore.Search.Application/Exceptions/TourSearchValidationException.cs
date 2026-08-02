using System;
using System.Collections.Generic;
using System.Linq;

namespace TourCore.Search.Application.Exceptions
{
    public sealed class TourSearchValidationException : Exception
    {
        public TourSearchValidationException(IEnumerable<string> errors) : base("Search request is invalid.")
        {
            Errors = errors == null ? new string[0] : errors.ToArray();
        }
        public IReadOnlyCollection<string> Errors { get; }
    }
}
