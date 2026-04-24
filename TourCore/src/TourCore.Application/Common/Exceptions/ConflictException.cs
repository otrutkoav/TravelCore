using System;

namespace TourCore.Application.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message)
        {
        }
    }
}