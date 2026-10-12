namespace TourCore.Application.Abstractions.Services
{
    public interface IUserContext
    {
        string UserId { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
    }
}