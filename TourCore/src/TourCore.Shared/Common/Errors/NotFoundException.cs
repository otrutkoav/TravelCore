namespace TourCore.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationExceptionBase
    {
        public NotFoundException(string message) : this(message, "not_found") { }
        public NotFoundException(string message, string code) : base(message, code) { }
    }
}
