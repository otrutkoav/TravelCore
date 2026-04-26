using System;

namespace TourCore.Application.Common.Exceptions
{
    public abstract class ApplicationExceptionBase : Exception
    {
        public string Code { get; private set; }

        protected ApplicationExceptionBase(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}