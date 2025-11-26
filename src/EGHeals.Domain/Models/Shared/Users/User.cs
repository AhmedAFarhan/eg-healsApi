using EGHeals.Domain.Models.Shared.Applications;
using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class User : AuditableAggregate<UserId>
    {
        private readonly List<UserRole> _userRoles = new();
        private readonly List<UserClientApplication> _userClientApplications = new();

        public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();
        public IReadOnlyList<UserClientApplication> UserClientApplications => _userClientApplications.AsReadOnly();

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string NormalizedUserName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string NormalizedEmail { get; set; } = default!;
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; } = default!;
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RawPassword { get; private set; }

        public Tenant Tenant { get; set; } = default!;

        /***************************************** Domain Business *****************************************/

        public static User Create(UserId id,
                                  string firstName,
                                  string lastName,
                                  string? email,
                                  string? rawPassword = null,
                                  string? phoneNumber = null,
                                  bool isActive = true)
        {
            //Domain model validation
            if (rawPassword is not null)
            {
                if (rawPassword.Length < 6 || rawPassword.Length > 50)
                {
                    throw new ArgumentOutOfRangeException(nameof(email), email.Length, "password should be in range between 6 and 50 characters.");
                }
            }

            Validation(firstName, lastName, email, phoneNumber);           

            var user = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : phoneNumber,
                NormalizedUserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : phoneNumber,
                Email = email,
                NormalizedEmail = email?.ToUpperInvariant(),
                PhoneNumber = phoneNumber,
                RawPassword = rawPassword,
                IsActive = isActive
            };

            return user;
        }

        public void Update(string firstName,
                           string lastName,
                           string? email,
                           string? phoneNumber = null)
        {
            //Domain model validation
            Validation(firstName, lastName, email, phoneNumber);

            FirstName = firstName;
            LastName = lastName;
            UserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : phoneNumber;
            NormalizedUserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : phoneNumber;
            Email = email;
            NormalizedEmail = email?.ToUpperInvariant();
            PhoneNumber = phoneNumber;
        }

        public UserRole AddRole(RoleId roleId)
        {
            var userRole = new UserRole(Id, roleId);
            _userRoles.Add(userRole);
            return userRole;
        }
        public void RemoveRole(RoleId roleId)
        {
            var userRole = _userRoles.FirstOrDefault(x => x.RoleId == roleId);
            if (userRole is not null)
            {
                _userRoles.Remove(userRole);
            }
        }

        public void AddUserClientApp(ClientApplicationId clientAppId)
        {
            var userClientApp = new UserClientApplication(Id, clientAppId);
            _userClientApplications.Add(userClientApp);
        }
        public void RemoveUserClientApp(ClientApplicationId clientAppId)
        {
            var userClientApp = _userClientApplications.FirstOrDefault(x => x.ClientApplicationId == clientAppId);
            if (userClientApp is not null)
            {
                _userClientApplications.Remove(userClientApp);
            }
        }

        public void Activate()
        {
            if (IsActive)
            {
                throw new DomainException("User is already activated");
            }

            IsActive = true;
        }
        public void Deactivate()
        {
            if (!IsActive)
            {
                throw new DomainException("User is already deactivated");
            }

            IsActive = false;
        }

        public void ConfirmMobile()
        {
            if (PhoneNumberConfirmed)
            {
                throw new DomainException("Mobile is already confirmed");
            }

            PhoneNumberConfirmed = true;
        }
        public void ConfirmEmail()
        {
            if (EmailConfirmed)
            {
                throw new DomainException("Email is already confirmed");
            }

            EmailConfirmed = true;
        }

        private static void Validation(string firstName, string lastName, string? email, string? phoneNumber)
        {
            if ((string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) && (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrWhiteSpace(phoneNumber)))
            {
                throw new ArgumentException("email or mobile should be provided.");
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("firstName cannot be null, empty, or whitespace.", nameof(firstName));
            }

            if (firstName.Length < 3 || firstName.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(firstName), firstName.Length, "firstName should be in range between 3 and 150 characters.");
            }

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("lastName cannot be null, empty, or whitespace.", nameof(lastName));
            }

            if (lastName.Length < 3 || lastName.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(lastName), lastName.Length, "lastName should be in range between 3 and 150 characters.");
            }

            if (email is not null)
            {
                if (!email.Contains('@'))
                {
                    throw new ArgumentException("Invalid email address", nameof(email));
                }

                if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be null, empty, or whitespace.", nameof(email));
                }

                if (email.Length < 6 || email.Length > 150)
                {
                    throw new ArgumentOutOfRangeException(nameof(email), email.Length, "Email should be in range between 6 and 150 characters.");
                }
            }

            if (phoneNumber is not null)
            {
                if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrWhiteSpace(phoneNumber))
                {
                    throw new ArgumentException("Mobile cannot be null, empty, or whitespace.", nameof(phoneNumber));
                }

                if (!phoneNumber.All(char.IsDigit))
                {
                    throw new ArgumentException("Mobile must contain digits only.", nameof(phoneNumber));
                }

                if (phoneNumber.Length != 11)
                {
                    throw new ArgumentOutOfRangeException(nameof(phoneNumber), phoneNumber.Length, "Mobile number must be exactly 11 digits long.");
                }
            }      
        }

        /***************************************** Identity Helper Methods *****************************************/

        public void AddRolesRange(List<UserRole> roles) => _userRoles.AddRange(roles);
        public void AddClientAppsRange(List<UserClientApplication> clientApps) => _userClientApplications.AddRange(clientApps);

    }
}
