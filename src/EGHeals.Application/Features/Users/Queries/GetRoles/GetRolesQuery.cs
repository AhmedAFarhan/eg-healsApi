using EGHeals.Application.Dtos.Roles.Responses;

namespace EGHeals.Application.Features.Users.Queries.GetRoles
{
    public record GetRolesQuery() : IQuery<GetRolesResult>;
    public record GetRolesResult(EGResponse<IEnumerable<RoleResponseDto>> response);
}
