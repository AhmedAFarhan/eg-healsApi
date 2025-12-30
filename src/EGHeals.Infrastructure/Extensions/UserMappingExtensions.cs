using BuildingBlocks.DataAccessAbstraction.Models;
using BuildingBlocks.DataAccessAbstraction.Queries;
using EGHeals.Domain.Models.Shared.Users;

namespace EGHeals.Infrastructure.Extensions
{
    public static class UserMappingExtensions
    {
        public static User ToDomainUser(this AppUser appUser)
        {
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
                TenantId = appUser.TenantId,
                Tenant = appUser.Tenant,
                IsActive = appUser.IsActive
            };

            // Map permissions
            if (appUser.UserRoles is not null && appUser.UserRoles.Count > 0)
            {
                user.AddRolesRange(appUser.UserRoles.ToList());
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
            var appUser = new AppUser();

            appUser.CopyToIdentity(user);

            return appUser;
        }

        public static QueryOptions<AppUser> ToAppUserQueryOptions(this QueryOptions<User> domainOptions)
        {
            var appOptions = new QueryOptions<AppUser>
            {
                PageIndex = domainOptions.PageIndex,
                PageSize = domainOptions.PageSize,
                SortBy = domainOptions.SortBy,
                SortDescending = domainOptions.SortDescending,
                QueryFilters = new QueryFilters<AppUser>
                {
                    UseOrLogic = domainOptions.QueryFilters.UseOrLogic,
                    Filters = domainOptions.QueryFilters.Filters.Select(f => new FilterExpression
                    {
                        PropertyName = f.PropertyName,
                        Operator = f.Operator,
                        Value = f.Value
                    }).ToList()
                }
            };

            return appOptions;
        }

        public static QueryFilters<AppUser> ToAppUserQueryFilters(this QueryFilters<User> domainOptions)
        {
            var appOptions = new QueryFilters<AppUser>
            {
                UseOrLogic = domainOptions.UseOrLogic,
                Filters = domainOptions.Filters.Select(f => new FilterExpression
                {
                    PropertyName = f.PropertyName,
                    Operator = f.Operator,
                    Value = f.Value
                }).ToList()
            };

            return appOptions;
        }

        public static void CopyToIdentity(this AppUser appUser, User user)
        {
            appUser.Id = user.Id;
            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;
            appUser.UserName = user.UserName;
            appUser.NormalizedUserName = user.NormalizedUserName;
            appUser.Email = user.Email;
            appUser.NormalizedEmail = user.NormalizedEmail;
            appUser.TenantId = user.TenantId;
            appUser.Tenant = user.Tenant;
            appUser.PhoneNumber = user.PhoneNumber;
            appUser.IsActive = user.IsActive;

            // Map permissions
            if (user.UserRoles is not null && user.UserRoles.Count > 0)
            {
                appUser.AddRolesRange(user.UserRoles.ToList());
            }

            // Map client applications
            if (user.UserClientApplications is not null && user.UserClientApplications.Count > 0)
            {
                appUser.AddClientAppsRange(user.UserClientApplications.ToList());
            }
        }
    }
}
