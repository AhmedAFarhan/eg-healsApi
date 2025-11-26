using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class UserRole : AuditableEntity<UserRoleId>
    {
        public UserRole(UserId userId, RoleId roleId)
        {
            Id = UserRoleId.Of(Guid.NewGuid());
            UserId = userId;
            RoleId = roleId;
        }

        public UserId UserId { get; private set; } = default!;
        public RoleId RoleId { get; private set; } = default!;

        public Role Role { get; private set; } = default!; /* NAVAIGATION PROPERTY */
    }
}
