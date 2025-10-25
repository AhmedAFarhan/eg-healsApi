namespace BuildingBlocks.DataAccessAbstraction.Services
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets the current user's unique identifier (for example, user ID or subject claim).
        /// </summary>
        Guid? UserId { get; }

        /// <summary>
        /// Gets the current user's username or display name.
        /// </summary>
        string? Username { get; }

        /// <summary>
        /// Gets the current user's ownership id to indicate who owns this user.
        /// </summary>
        Guid? OwnershipId { get; }

        /// <summary>
        /// Gets the current user's roles.
        /// </summary>
        IEnumerable<string> GetRoles();
    }
}
