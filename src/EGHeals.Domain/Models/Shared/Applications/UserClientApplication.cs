using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Applications
{
    public class UserClientApplication : SystemEntity<UserClientApplicationId>
    {
        public UserClientApplication(UserId userId, ClientApplicationId clientApplicationId)
        {
            Id = UserClientApplicationId.Of(Guid.NewGuid());
            UserId = userId;
            ClientApplicationId = clientApplicationId;
        }

        public UserId UserId { get; set; } = default!;
        public ClientApplicationId ClientApplicationId { get; set; } = default!;
    }
}
