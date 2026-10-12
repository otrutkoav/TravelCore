using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Avia.Entities
{
    /// <summary>
    /// Воздушное судно / тип самолета.
    /// </summary>
    public class Aircraft : AuditableEntity
    {
        /// <summary>
        /// Код воздушного судна.
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// Основное название воздушного судна.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Альтернативное / латинское название воздушного судна.
        /// </summary>
        public string NameEn { get; protected set; }

        protected Aircraft()
        {
        }

        public Aircraft(
            string code,
            string name,
            DateTime createdAt,
            string nameEn = null)
        {
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);

            SetCreated(createdAt);
        }

        public void Update(
            string code,
            string name,
            DateTime updatedAt,
            string nameEn = null)
        {
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);

            SetUpdated(updatedAt);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Aircraft code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 3)
                throw new DomainException("Aircraft code must be 3 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Aircraft name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Aircraft name must be 100 characters or less.");

            Name = name;
        }

        private void SetNameEn(string nameEn)
        {
            if (string.IsNullOrWhiteSpace(nameEn))
            {
                NameEn = null;
                return;
            }

            nameEn = nameEn.Trim();

            if (nameEn.Length > 100)
                throw new DomainException("Aircraft alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }
    }
}