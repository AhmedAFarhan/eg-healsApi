namespace BuildingBlocks.DataAccessAbstraction.Services
{
    public interface IUserContextService
    {
        /// <summary>
        /// Gets the current user's unique identifier (for example, user ID or subject claim).
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Gets the current user's ownership id to indicate who owns this user.
        /// </summary>
        Guid TenantId { get; }

        /// <summary>
        /// Gets the current user's activity id to indicate the user type.
        /// </summary>
        string Activity { get; }

        /// <summary>
        /// Gets the current user's activity id to indicate the user type.
        /// </summary>
        bool IsSystemUser { get; }

        /// <summary>
        /// Gets the current user's roles.
        /// </summary>
        IEnumerable<string> GetRoles();
    }
}
