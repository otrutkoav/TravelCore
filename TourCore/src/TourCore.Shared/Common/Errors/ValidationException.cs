using System.Collections.Generic;
using System.Linq;

namespace TourCore.Application.Common.Exceptions
{
    public class ValidationException : ApplicationExceptionBase
    {
        public ValidationException(string message) : this(new Dictionary<string, ValidationError[]> { { "General", new[] { new ValidationError("validation_error", message) } } }) { }
        public ValidationException(IEnumerable<string> errors) : this(new Dictionary<string, ValidationError[]> { { "General", (errors ?? Enumerable.Empty<string>()).Select(message => new ValidationError("validation_error", message)).ToArray() } }) { }
        public ValidationException(IReadOnlyDictionary<string, string[]> errors) : this((errors ?? new Dictionary<string, string[]>()).ToDictionary(pair => pair.Key, pair => (pair.Value ?? new string[0]).Select(code => new ValidationError(code)).ToArray())) { }
        public ValidationException(IReadOnlyDictionary<string, ValidationError[]> errors) : base("Ошибка валидации.", "validation_error") { Errors = errors ?? new Dictionary<string, ValidationError[]>(); }
        public IReadOnlyDictionary<string, ValidationError[]> Errors { get; }
    }
}
