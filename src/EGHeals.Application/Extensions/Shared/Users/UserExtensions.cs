using EGHeals.Application.Dtos.Shared.Users.Responses;
using EGHeals.Domain.Models.Shared.Users;
using System.Data;

namespace EGHeals.Application.Extensions.Shared.Users
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
                Roles: user.UserRoles.Select(role => new UserRoleResponseDto(Id: role.Id.Value, Name: role.Role.Name))
            );
        }
        public static IEnumerable<UserResponseDto> ToSubUsersDtos(this IEnumerable<User> users)
        {
            return users.Select(user => new UserResponseDto
            (
                Id: user.Id.Value,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Username: user.UserName,
                Email: user.Email,
                PhoneNumber: user.PhoneNumber,
                Roles: user.UserRoles.Select(role=> new UserRoleResponseDto(Id: role.Id.Value, Name: role.Role.Name))
            ));
        }
    }
}
