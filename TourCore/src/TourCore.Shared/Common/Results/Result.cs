using System;
using TourCore.Shared.Common.Errors;

namespace TourCore.Shared.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; private set; }

        public bool IsFailure
        {
            get { return !IsSuccess; }
        }

        public Error Error { get; private set; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException("Successful result cannot contain an error.");

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException("Failure result must contain an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }

        public static Result<TValue> Success<TValue>(TValue value)
        {
            return Result<TValue>.Success(value);
        }

        public static Result<TValue> Failure<TValue>(Error error)
        {
            return Result<TValue>.Failure(error);
        }
    }
}