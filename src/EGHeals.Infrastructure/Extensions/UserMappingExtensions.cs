using EGHeals.Domain.Models.Shared.Users;
using System.Reflection;

namespace EGHeals.Infrastructure.Extensions
{
    public static class UserMappingExtensions
    {
        public static User ToDomainUser(this AppUser appUser)
        {
            if (appUser is null) return null;

            var user = new User
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                UserName = appUser.UserName,
                NormalizedUserName = appUser.NormalizedUserName,
                Email = appUser.Email,
                NormalizedEmail = appUser.NormalizedEmail,
                PhoneNumber = appUser.PhoneNumber,
                OwnershipId = appUser.OwnershipId,
                IsActive = appUser.IsActive
            };

            // Map permissions
            if (appUser.UserPermissions is not null && appUser.UserPermissions.Count > 0)
            {
                user.AddPermissionsRange(appUser.UserPermissions.ToList());
            }

            // Map client applications
            if (appUser.UserClientApplications is not null && appUser.UserClientApplications.Count > 0)
            {
                user.AddClientAppsRange(appUser.UserClientApplications.ToList());
            }

            return user;
        }

        public static AppUser ToIdentityUser(this User user)
        {
            if (user is null) return null;

            var appUser = new AppUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                NormalizedUserName = user.NormalizedUserName,
                Email = user.Email,
                NormalizedEmail = user.NormalizedEmail,
                OwnershipId = user.OwnershipId,
                PhoneNumber = user.PhoneNumber,
            };

            // Map permissions
            if (user.UserPermissions is not null && user.UserPermissions.Count > 0)
            {
                appUser.AddPermissionsRange(user.UserPermissions.ToList());
            }

            // Map client applications
            if (user.UserClientApplications is not null && user.UserClientApplications.Count > 0)
            {
                appUser.AddClientAppsRange(user.UserClientApplications.ToList());
            }

            return appUser;
        }

        public static void CopyToIdentity(this AppUser appUser, User user)
        {

        }
    }
}
