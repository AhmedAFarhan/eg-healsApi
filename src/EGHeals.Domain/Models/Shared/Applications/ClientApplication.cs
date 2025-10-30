using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Applications
{
    public class ClientApplication : SystemEntity<ClientApplicationId>
    {
        public string Client { get; set; } = default!;

        public static ClientApplication Create(ClientApplicationId id, string client)
        {
            if (string.IsNullOrEmpty(client) || string.IsNullOrWhiteSpace(client))
            {
                throw new ArgumentException("client cannot be null, empty, or whitespace.", nameof(client));
            }

            if (client.Length < 3 || client.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(client), client.Length, "client should be in range between 3 and 150 characters.");
            }

            var clientApp = new ClientApplication
            {
                Id = id,
                Client = client,
            };

            return clientApp;
        }
    }
}
