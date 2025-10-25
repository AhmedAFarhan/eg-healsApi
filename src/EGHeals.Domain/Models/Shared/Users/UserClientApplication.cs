using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class UserClientApplication : SystemEntity<UserClientApplicationId>
    {
        public UserClientApplication(SystemUserId systemUserId, ClientApplicationId clientApplicationId)
        {
            Id = UserClientApplicationId.Of(Guid.NewGuid());
            SystemUserId = systemUserId;
            ClientApplicationId = clientApplicationId;
        }

        public SystemUserId SystemUserId { get; set; } = default!;
        public ClientApplicationId ClientApplicationId { get; set; } = default!;
    }
}
