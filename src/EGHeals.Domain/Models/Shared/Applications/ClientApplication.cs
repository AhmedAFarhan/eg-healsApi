using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Applications
{
    public class ClientApplication 
    {
        public ClientApplicationId Id { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string ClientSecretHash { get; set; } = default!;
        public Platform Platform { get; set; }
        public bool IsActive { get; set; } = true;

        public static ClientApplication Create(ClientApplicationId id, string clientId, Platform platform)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentException("clientId cannot be null, empty, or whitespace.", nameof(clientId));
            }

            if (clientId.Length < 3 || clientId.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(clientId), clientId.Length, "clientId should be in range between 3 and 150 characters.");
            }

            if (!Enum.IsDefined(typeof(Platform), platform))
            {
                throw new ArgumentException("Platform is out of range.", nameof(platform));
            }

            var clientApp = new ClientApplication
            {
                Id = id,
                ClientId = clientId,
                Platform = platform,
                IsActive = true
            };

            return clientApp;
        }

        public void SetClientSecretHash(string clientSecretHash) => ClientSecretHash = clientSecretHash;
    }
}
