using System.Collections.Generic;

namespace TourCore.Search.Api.Legacy.Models
{
    public sealed class SearchValidationErrorResponse
    {
        public SearchValidationErrorResponse(string message, IReadOnlyCollection<string> errors)
        {
            Message = message;
            Errors = errors;
        }

        public string Message { get; }
        public IReadOnlyCollection<string> Errors { get; }
    }
}
