using System;

namespace TourCore.Shared.Common.Errors
{
    public sealed class Error
    {
        public static readonly Error None = new Error(string.Empty, string.Empty);

        public string Code { get; private set; }

        public string Message { get; private set; }

        private Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public static Error Create(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Error code cannot be empty.", "code");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Error message cannot be empty.", "message");

            return new Error(code, message);
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Code)
                ? Message
                : Code + ": " + Message;
        }
    }
}