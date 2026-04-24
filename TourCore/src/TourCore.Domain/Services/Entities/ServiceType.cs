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

        public short? Category { get; protected set; }
        public int? ControlMode { get; protected set; }

        public bool IsCity { get; protected set; }
        public bool HasPrimaryParameter { get; protected set; }
        public bool HasSecondaryParameter { get; protected set; }
        public bool RoundGrossAmount { get; protected set; }

        public bool IsDuration { get; protected set; }
        public bool UseManualInput { get; protected set; }
        public bool IsQuoted { get; protected set; }
        public bool IsIndividual { get; protected set; }

        public decimal? SmallAmountPercent { get; protected set; }
        public short? SmallAmountThreshold { get; protected set; }
        public bool UseSmallAmountAndRule { get; protected set; }

        public bool IsRoute { get; protected set; }
        public bool IsPartnerBasedOn { get; protected set; }

        protected ServiceType()
        {
        }

        public ServiceType(
            string name,
            DateTime createdAt,
            string code = null,
            string nameEn = null,
            short? category = null,
            int? controlMode = null,
            bool isCity = false,
            bool hasPrimaryParameter = false,
            bool hasSecondaryParameter = false,
            bool roundGrossAmount = false,
            bool isDuration = false,
            bool useManualInput = false,
            bool isQuoted = false,
            bool isIndividual = false,
            decimal? smallAmountPercent = null,
            short? smallAmountThreshold = null,
            bool useSmallAmountAndRule = false,
            bool isRoute = false,
            bool isPartnerBasedOn = false)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetCategory(category);
            SetControlMode(controlMode);
            SetIsCity(isCity);
            SetHasPrimaryParameter(hasPrimaryParameter);
            SetHasSecondaryParameter(hasSecondaryParameter);
            SetRoundGrossAmount(roundGrossAmount);
            SetIsDuration(isDuration);
            SetUseManualInput(useManualInput);
            SetIsQuoted(isQuoted);
            SetIsIndividual(isIndividual);
            SetSmallAmountPercent(smallAmountPercent);
            SetSmallAmountThreshold(smallAmountThreshold);
            SetUseSmallAmountAndRule(useSmallAmountAndRule);
            SetIsRoute(isRoute);
            SetIsPartnerBasedOn(isPartnerBasedOn);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string code = null,
            string nameEn = null,
            short? category = null,
            int? controlMode = null,
            bool isCity = false,
            bool hasPrimaryParameter = false,
            bool hasSecondaryParameter = false,
            bool roundGrossAmount = false,
            bool isDuration = false,
            bool useManualInput = false,
            bool isQuoted = false,
            bool isIndividual = false,
            decimal? smallAmountPercent = null,
            short? smallAmountThreshold = null,
            bool useSmallAmountAndRule = false,
            bool isRoute = false,
            bool isPartnerBasedOn = false)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetCategory(category);
            SetControlMode(controlMode);
            SetIsCity(isCity);
            SetHasPrimaryParameter(hasPrimaryParameter);
            SetHasSecondaryParameter(hasSecondaryParameter);
            SetRoundGrossAmount(roundGrossAmount);
            SetIsDuration(isDuration);
            SetUseManualInput(useManualInput);
            SetIsQuoted(isQuoted);
            SetIsIndividual(isIndividual);
            SetSmallAmountPercent(smallAmountPercent);
            SetSmallAmountThreshold(smallAmountThreshold);
            SetUseSmallAmountAndRule(useSmallAmountAndRule);
            SetIsRoute(isRoute);
            SetIsPartnerBasedOn(isPartnerBasedOn);

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

        private void SetCategory(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Service type category cannot be negative.");

            Category = value;
        }

        private void SetControlMode(int? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Service type control mode cannot be negative.");

            ControlMode = value;
        }

        private void SetIsCity(bool value)
        {
            IsCity = value;
        }

        private void SetHasPrimaryParameter(bool value)
        {
            HasPrimaryParameter = value;
        }

        private void SetHasSecondaryParameter(bool value)
        {
            HasSecondaryParameter = value;
        }

        private void SetRoundGrossAmount(bool value)
        {
            RoundGrossAmount = value;
        }

        private void SetIsDuration(bool value)
        {
            IsDuration = value;
        }

        private void SetUseManualInput(bool value)
        {
            UseManualInput = value;
        }

        private void SetIsQuoted(bool value)
        {
            IsQuoted = value;
        }

        private void SetIsIndividual(bool value)
        {
            IsIndividual = value;
        }

        private void SetSmallAmountPercent(decimal? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Service type small amount percent cannot be negative.");

            SmallAmountPercent = value;
        }

        private void SetSmallAmountThreshold(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Service type small amount threshold cannot be negative.");

            SmallAmountThreshold = value;
        }

        private void SetUseSmallAmountAndRule(bool value)
        {
            UseSmallAmountAndRule = value;
        }

        private void SetIsRoute(bool value)
        {
            IsRoute = value;
        }

        private void SetIsPartnerBasedOn(bool value)
        {
            IsPartnerBasedOn = value;
        }
    }
}