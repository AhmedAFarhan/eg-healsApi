using EGHeals.Domain.ValueObjects.Shared.Users;

namespace EGHeals.Domain.Models.Shared.Users
{
    public class SystemUser : Aggregate<SystemUserId>
    {
        private readonly List<UserRole> _userRoles = new();
        private readonly List<UserClientApplication> _userClientApplications = new();

        public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();
        public IReadOnlyList<UserClientApplication> UserClientApplications => _userClientApplications.AsReadOnly();

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;       
        public string UserName { get; private set; } = default!;
        public string NormalizedUserName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; private set; } = false;
        public string? Mobile { get; private set; } = default!;
        public bool MobileConfirmed { get; private set; } = false;
        public bool IsActive { get; set; } = true;


        /***************************************** Domain Business *****************************************/

        public static SystemUser Create(SystemUserId id,
                                        string firstName,
                                        string lastName,
                                        string? email,
                                        string? mobile = null,
                                        bool isActive = true)
        {
            //Domain model validation
            Validation(firstName, lastName, email, mobile);           

            var user = new SystemUser
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : mobile,
                NormalizedUserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : mobile,
                Email = email,
                NormalizedEmail = email?.ToUpperInvariant(),
                Mobile = mobile,
                IsActive = isActive
            };

            return user;
        }
        public void Update(string firstName,
                           string lastName,
                           string? email,
                           string? mobile = null)
        {
            //Domain model validation
            Validation(firstName, lastName, email, mobile);

            FirstName = firstName;
            LastName = lastName;
            UserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : mobile;
            NormalizedUserName = !string.IsNullOrEmpty(email) ? email.ToUpperInvariant() : mobile;
            Email = email;
            NormalizedEmail = email?.ToUpperInvariant();
            Mobile = mobile;
        }

        public UserRole AddUserRole(RoleId roleId)
        {
            var userRole = new UserRole(Id, roleId);
            _userRoles.Add(userRole);
            return userRole;
        }
        public void RemoveUserRole(RoleId roleId)
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
            if (MobileConfirmed)
            {
                throw new DomainException("Mobile is already confirmed");
            }

            MobileConfirmed = true;
        }
        public void ConfirmEmail()
        {
            if (EmailConfirmed)
            {
                throw new DomainException("Email is already confirmed");
            }

            EmailConfirmed = true;
        }

        private static void Validation(string firstName, string lastName, string? email, string? mobile)
        {
            if ((string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) && (string.IsNullOrEmpty(mobile) || string.IsNullOrWhiteSpace(mobile)))
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

            if (mobile is not null)
            {
                if (string.IsNullOrEmpty(mobile) || string.IsNullOrWhiteSpace(mobile))
                {
                    throw new ArgumentException("Mobile cannot be null, empty, or whitespace.", nameof(mobile));
                }

                if (!mobile.All(char.IsDigit))
                {
                    throw new ArgumentException("Mobile must contain digits only.", nameof(mobile));
                }

                if (mobile.Length != 11)
                {
                    throw new ArgumentOutOfRangeException(nameof(mobile), mobile.Length, "Mobile number must be exactly 11 digits long.");
                }
            }      
        }

    }
}
