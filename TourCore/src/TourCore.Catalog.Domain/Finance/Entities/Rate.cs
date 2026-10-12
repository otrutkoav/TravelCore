using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Finance.Entities
{
    /// <summary>
    /// Валюта / расчетная единица.
    /// </summary>
    public class Rate : AuditableEntity
    {
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string IsoCode { get; protected set; }

        public bool IsMain { get; protected set; }
        public bool IsNational { get; protected set; }
        public bool ShowInSearch { get; protected set; }

        /// <summary>
        /// Символ валюты. Пока храним как есть.
        /// </summary>
        public byte[] Symbol { get; protected set; }

        protected Rate()
        {
        }

        public Rate(
            string code,
            string name,
            DateTime createdAt,
            string isoCode = null,
            bool isMain = false,
            bool isNational = false,
            bool showInSearch = false,
            byte[] symbol = null)
        {
            SetCode(code);
            SetName(name);
            SetIsoCode(isoCode);
            SetIsMain(isMain);
            SetIsNational(isNational);
            SetShowInSearch(showInSearch);
            SetSymbol(symbol);

            SetCreated(createdAt);
        }

        public void Update(
            string code,
            string name,
            DateTime updatedAt,
            string isoCode = null,
            bool isMain = false,
            bool isNational = false,
            bool showInSearch = false,
            byte[] symbol = null)
        {
            SetCode(code);
            SetName(name);
            SetIsoCode(isoCode);
            SetIsMain(isMain);
            SetIsNational(isNational);
            SetShowInSearch(showInSearch);
            SetSymbol(symbol);

            SetUpdated(updatedAt);
        }

        private void SetCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Rate code is required.");

            value = value.Trim();

            if (value.Length > 3)
                throw new DomainException("Rate code must be 3 characters or less.");

            Code = value;
        }

        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Rate name is required.");

            value = value.Trim();

            if (value.Length > 50)
                throw new DomainException("Rate name must be 50 characters or less.");

            Name = value;
        }

        private void SetIsoCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                IsoCode = null;
                return;
            }

            value = value.Trim().ToUpperInvariant();

            if (value.Length > 3)
                throw new DomainException("Rate ISO code must be 3 characters or less.");

            IsoCode = value;
        }

        private void SetIsMain(bool value)
        {
            IsMain = value;
        }

        private void SetIsNational(bool value)
        {
            IsNational = value;
        }

        private void SetShowInSearch(bool value)
        {
            ShowInSearch = value;
        }

        private void SetSymbol(byte[] value)
        {
            Symbol = value;
        }
    }
}