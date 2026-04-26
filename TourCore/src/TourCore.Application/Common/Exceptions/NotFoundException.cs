using System;

namespace TourCore.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Code { get; }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, string code)
            : base(message)
        {
            Code = code;
        }
    }
}