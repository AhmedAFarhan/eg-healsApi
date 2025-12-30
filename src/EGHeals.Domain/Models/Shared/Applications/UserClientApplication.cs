using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Applications
{
    public class UserClientApplication
    {
        public UserClientApplication(UserId userId, ClientApplicationId clientApplicationId)
        {
            Id = UserClientApplicationId.Of(Guid.NewGuid());
            UserId = userId;
            ClientApplicationId = clientApplicationId;
        }

        public UserClientApplicationId Id { get; set; } = default!;
        public UserId UserId { get; set; } = default!;
        public ClientApplicationId ClientApplicationId { get; set; } = default!;
    }
}
