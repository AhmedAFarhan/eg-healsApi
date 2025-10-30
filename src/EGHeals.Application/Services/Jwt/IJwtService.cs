using EGHeals.Application.Dtos.Users.Responses;

namespace EGHeals.Application.Services.Jwt
{
    public interface IJwtService
    {
        public string GenerateToken(UserResponseDto user);
    }
}
