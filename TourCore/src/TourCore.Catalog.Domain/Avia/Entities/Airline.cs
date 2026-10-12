using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Avia.Entities
{
    /// <summary>
    /// Авиакомпания.
    /// </summary>
    public class Airline : AuditableEntity
    {
        /// <summary>
        /// Основной код авиакомпании.
        /// Обычно используется как краткий код в системе.
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// Основное название авиакомпании.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Альтернативное / латинское название авиакомпании.
        /// </summary>
        public string NameEn { get; protected set; }

        /// <summary>
        /// ICAO-код авиакомпании.
        /// </summary>
        public string IcaoCode { get; protected set; }

        protected Airline()
        {
        }

        public Airline(
            string code,
            string name,
            DateTime createdAt,
            string nameEn = null,
            string icaoCode = null)
        {
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);
            SetIcaoCode(icaoCode);

            SetCreated(createdAt);
        }

        public void Update(
            string code,
            string name,
            DateTime updatedAt,
            string nameEn = null,
            string icaoCode = null)
        {
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);
            SetIcaoCode(icaoCode);

            SetUpdated(updatedAt);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Airline code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 3)
                throw new DomainException("Airline code must be 3 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Airline name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Airline name must be 100 characters or less.");

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
                throw new DomainException("Airline alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetIcaoCode(string icaoCode)
        {
            if (string.IsNullOrWhiteSpace(icaoCode))
            {
                IcaoCode = null;
                return;
            }

            icaoCode = icaoCode.Trim().ToUpperInvariant();

            if (icaoCode.Length > 4)
                throw new DomainException("Airline ICAO code must be 4 characters or less.");

            IcaoCode = icaoCode;
        }
    }
}