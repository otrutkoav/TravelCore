namespace TourCore.Application.Abstractions.Validation
{
    public interface IValidationService
    {
        void ValidateAndThrow(object instance);
    }
}