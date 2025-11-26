using EGHeals.Application.Dtos.Shared.Users.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Services.Users
{
    public interface IUserQueryService
    {
        Task<IEnumerable<UserResponseDto>> GetSubUsersWithRolesAsync(QueryOptions<User> options,
                                                                        bool includeOwnership = false,
                                                                        CancellationToken cancellationToken = default);
    }
}
