using EGHeals.Application.Dtos.Users.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Services.Users
{
    public interface IUserQueryService
    {
        Task<UserResponseDto?> GetUserWithPermissions(UserId id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SubUserResponseDto>> GetSubUsersAsync(QueryOptions<User> options,
                                                                            bool includeOwnership = false,
                                                                            CancellationToken cancellationToken = default);
    }
}
