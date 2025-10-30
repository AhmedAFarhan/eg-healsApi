using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class UserPermission : Entity<UserPermissionId>
    {
        public UserPermission(UserId userId, PermissionId permissionId)
        {
            Id = UserPermissionId.Of(Guid.NewGuid());
            UserId = userId;
            PermissionId = permissionId;
        }

        public UserId UserId { get; private set; } = default!;
        public PermissionId PermissionId { get; private set; } = default!;

        public Permission Permission { get; private set; } = default!; /* NAVAIGATION PROPERTY */
    }
}
