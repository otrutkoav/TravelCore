using System;
using TourCore.Shared.Common.Errors;

namespace TourCore.Shared.Common.Results
{
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public TValue Value
        {
            get
            {
                if (IsFailure)
                    throw new InvalidOperationException("Cannot access value of failed result.");

                return _value;
            }
        }

        private Result(TValue value)
            : base(true, Error.None)
        {
            _value = value;
        }

        private Result(Error error)
            : base(false, error)
        {
            _value = default(TValue);
        }

        public static Result<TValue> Success(TValue value)
        {
            return new Result<TValue>(value);
        }

        public static new Result<TValue> Failure(Error error)
        {
            return new Result<TValue>(error);
        }
    }
}