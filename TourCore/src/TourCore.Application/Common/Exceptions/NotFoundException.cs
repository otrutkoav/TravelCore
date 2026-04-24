using System;

namespace TourCore.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}