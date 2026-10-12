using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Services.Entities
{
    /// <summary>
    /// Тип услуги.
    /// Описывает вид услуги и его поведение в системе.
    /// </summary>
    public class ServiceType : AuditableEntity
    {
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public string Code { get; protected set; }
        public bool IsDuration { get; protected set; }
        public bool IsRoute { get; protected set; }

        protected ServiceType()
        {
        }

        public ServiceType(
            string name,
            DateTime createdAt,
            string code = null,
            string nameEn = null,
            bool isDuration = false,
            bool isRoute = false)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetIsDuration(isDuration);
            SetIsRoute(isRoute);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string code = null,
            string nameEn = null,
            bool isDuration = false,
            bool isRoute = false)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetIsDuration(isDuration);
            SetIsRoute(isRoute);

            SetUpdated(updatedAt);
        }

        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Service type name is required.");

            value = value.Trim();

            if (value.Length > 50)
                throw new DomainException("Service type name must be 50 characters or less.");

            Name = value;
        }

        private void SetNameEn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                NameEn = null;
                return;
            }

            value = value.Trim();

            if (value.Length > 50)
                throw new DomainException("Service type alternate name must be 50 characters or less.");

            NameEn = value;
        }

        private void SetCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Code = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 10)
                throw new DomainException("Service type code must be 10 characters or less.");

            Code = value;
        }

        private void SetIsDuration(bool value)
        {
            IsDuration = value;
        }

        private void SetIsRoute(bool value)
        {
            IsRoute = value;
        }

    }
}
