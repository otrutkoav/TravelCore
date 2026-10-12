using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Tours
{
    public class TourServiceCommandValidator
    {
        public void Validate(CreateTourServiceCommand c)
        {
            var e = new List<string>();
            if (c == null)
                e.Add("Request body is required.");
            else
            {
                if (c.ServiceTypeId <= 0)
                    e.Add("ServiceTypeId must be greater than zero.");
                ValidateEditableFields(
                    c.Name,
                    c.ParentServiceId,
                    c.IsHidden,
                    c.IsRemovable,
                    c.IsOptional,
                    e
                );
            }
            if (e.Count > 0)
                throw new ValidationException(e);
        }

        public void Validate(UpdateTourServiceCommand c)
        {
            var e = new List<string>();
            if (c == null)
                e.Add("Request body is required.");
            else
                ValidateEditableFields(
                    c.Name,
                    c.ParentServiceId,
                    c.IsHidden,
                    c.IsRemovable,
                    c.IsOptional,
                    e
                );
            if (e.Count > 0)
                throw new ValidationException(e);
        }

        public void Validate(ReorderTourServicesCommand c)
        {
            if (c == null || c.ServiceIds == null)
                throw new ValidationException(new[] { "ServiceIds is required." });
            if (
                c.ServiceIds.Any(x => x <= 0)
                || c.ServiceIds.Count != c.ServiceIds.Distinct().Count()
            )
                throw new ValidationException(
                    new[] { "ServiceIds must contain unique positive identifiers." }
                );
        }

        private static void ValidateEditableFields(
            string name,
            int? parentServiceId,
            bool isHidden,
            bool isRemovable,
            bool isOptional,
            ICollection<string> errors
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                errors.Add("Name is required.");
            else if (name.Trim().Length > 256)
                errors.Add("Name must be 256 characters or less.");
            if (parentServiceId.HasValue && parentServiceId <= 0)
                errors.Add("ParentServiceId must be greater than zero.");
            if (isHidden && isRemovable)
                errors.Add("A hidden service cannot be removable.");
            if (isHidden && isOptional)
                errors.Add("A hidden service cannot be optional.");
        }
    }
}
