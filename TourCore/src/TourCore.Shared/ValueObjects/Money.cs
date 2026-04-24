using System;
using System.Collections.Generic;
using TourCore.Shared.Primitives;

namespace TourCore.Shared.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        private Money()
        {
        }

        public Money(decimal amount, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty.", "currency");

            Amount = amount;
            Currency = currency.Trim().ToUpperInvariant();
        }

        public static Money Zero(string currency)
        {
            return new Money(0m, currency);
        }

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);

            return new Money(Amount - other.Amount, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot operate with money in different currencies.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            return Amount + " " + Currency;
        }
    }
}