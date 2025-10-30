using EGHeals.Application.Dtos.Roles.Responses;
using EGHeals.Application.Dtos.Users;
using EGHeals.Application.Dtos.Users.Responses;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Application.Extensions.Users
{
    public static class UserExtensions
    {
        public static UserResponseDto ToUserDto(this User user)
        {
            return new UserResponseDto
            (
                Id: user.Id.Value,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Username: user.UserName,
                Email: user.Email,
                PhoneNumber: user.PhoneNumber,
                OwnershipId: user.OwnershipId.Value,
                Permissions: user.UserPermissions.Select(permission => new PermissionResponseDto
                (
                    Id: permission.Id.Value,
                    Name: permission.Permission.Name
                ))
            );
        }
        public static IEnumerable<SubUserResponseDto> ToSubUsersDtos(this IEnumerable<User> users)
        {
            return users.Select(user => new SubUserResponseDto
            (
                Id: user.Id.Value,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Username: user.UserName,
                Email: user.Email,
                PhoneNumber: user.PhoneNumber
            ));
        }
    }
}
