using System;

namespace TourCore.Application.Common.Exceptions
{
    public abstract class ApplicationExceptionBase : Exception
    {
        protected ApplicationExceptionBase(string message, string code) : base(message) { Code = code; }
        public string Code { get; }
    }
}
