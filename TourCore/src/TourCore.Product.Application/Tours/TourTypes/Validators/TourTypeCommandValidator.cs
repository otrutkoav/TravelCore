using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Tours
{
    public class TourTypeCommandValidator
    {
        public void ValidateAndThrow(string name, int? parentId)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(name))
                errors.Add("Name is required.");
            else if (name.Trim().Length > 128)
                errors.Add("Name must be 128 characters or less.");
            if (parentId.HasValue && parentId <= 0)
                errors.Add("ParentId must be greater than zero.");
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }
    }
}
