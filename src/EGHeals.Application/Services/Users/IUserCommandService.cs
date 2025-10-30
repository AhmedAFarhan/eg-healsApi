using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Services.Users
{
    public interface IUserCommandService
    {
        Task<User?> ActivateAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> DeactivateAsync(User user, CancellationToken cancellationToken = default);
    }
}
