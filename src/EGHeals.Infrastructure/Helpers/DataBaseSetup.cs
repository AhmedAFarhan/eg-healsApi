using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.Helpers
{
    public class DataBaseSetup(ApplicationDbContext dbContext, ApplicationIdentityDbContext identityDbContext, UserManager<ApplicationUser> userManager)
    {
        public async Task SetupAsync()
        {
            try
            {
                await SetupSqlDatabase();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private async Task SetupSqlDatabase()
        {
            // Apply migrations
            await dbContext.Database.MigrateAsync();

            // Apply identity migrations
            await identityDbContext.Database.MigrateAsync();

            // Seed data if database is empty
            var existRoles = await dbContext.Roles.AnyAsync();

            if (!existRoles)
            {
                dbContext.IsSeeding = true;

                /******************************* Defined Permissions *********************************/

                var permissionId_1 = PermissionId.Of(Guid.NewGuid());
                var permission_1 = Permission.Create(permissionId_1, "Permission 1");

                var permissionId_2 = PermissionId.Of(Guid.NewGuid());
                var permission_2 = Permission.Create(permissionId_2, "Permission 2");

                var permissionId_3 = PermissionId.Of(Guid.NewGuid());
                var permission_3 = Permission.Create(permissionId_3, "Permission 3");

                var permissionId_4 = PermissionId.Of(Guid.NewGuid());
                var permission_4 = Permission.Create(permissionId_4, "Permission 4");

                var permissionId_5 = PermissionId.Of(Guid.NewGuid());
                var permission_5 = Permission.Create(permissionId_5, "Permission 5");

                var permissionId_6 = PermissionId.Of(Guid.NewGuid());
                var permission_6 = Permission.Create(permissionId_6, "Permission 6");


                /******************************* Defined Roles *********************************/

                var superAdminRoleId = RoleId.Of(Guid.NewGuid());
                var superAdminRole = Role.Create(superAdminRoleId, "SuperAdmin", null, true);

                var radiologyCenterAdminRoleId = RoleId.Of(Guid.NewGuid());
                var radiologyCenterAdminRole = Role.Create(radiologyCenterAdminRoleId, "RadiologyCenterAdmin", UserActivity.RADIOLOGY, true);

                var radiologyCenterReceptionistRoleId = RoleId.Of(Guid.NewGuid());
                var radiologyCenterReceptionistRole = Role.Create(radiologyCenterReceptionistRoleId, "Receptionist", UserActivity.RADIOLOGY, false);

                radiologyCenterReceptionistRole.AddPermission(permissionId_1);
                radiologyCenterReceptionistRole.AddPermission(permissionId_2);

                var radiologyCenterRadiologistRoleId = RoleId.Of(Guid.NewGuid());
                var radiologyCenterRadiologistRole = Role.Create(radiologyCenterRadiologistRoleId, "Radiologist", UserActivity.RADIOLOGY, false);

                radiologyCenterRadiologistRole.AddPermission(permissionId_3);
                radiologyCenterRadiologistRole.AddPermission(permissionId_4);

                var radiologyCenterAccountantRoleId = RoleId.Of(Guid.NewGuid());
                var radiologyCenterAccountantRole = Role.Create(radiologyCenterAccountantRoleId, "Accountant", UserActivity.RADIOLOGY, false);

                radiologyCenterAccountantRole.AddPermission(permissionId_5);
                radiologyCenterAccountantRole.AddPermission(permissionId_6);


                /******************************* Defined System Users *********************************/

                var superAdminUserId = SystemUserId.Of(Guid.NewGuid());
                var superAdminUser = SystemUser.Create(superAdminUserId, "first name", "last name", "super@super.com", "01099315900");
                superAdminUser.OwnershipId = superAdminUserId;

                var radiologyCenterAdminUserId = SystemUserId.Of(Guid.NewGuid());
                var radiologyCenterAdminUser = SystemUser.Create(radiologyCenterAdminUserId, "first name", "last name", "radiology@radiology.com", "01096513165");
                radiologyCenterAdminUser.OwnershipId = radiologyCenterAdminUserId;

                var radiologyCenterSubUserId = SystemUserId.Of(Guid.NewGuid());
                var radiologyCenterSubUser = SystemUser.Create(radiologyCenterSubUserId, "first name", "last name", "sub@sub.com", "01093266551");
                radiologyCenterSubUser.OwnershipId = radiologyCenterAdminUserId;


                /******************************* Assign Roles To Users *********************************/

                var addSuperAdminRole = superAdminUser.AddUserRole(superAdminRoleId);
                addSuperAdminRole.OwnershipId = superAdminUserId;

                var addedRadiologyCenterAdminRole = radiologyCenterAdminUser.AddUserRole(radiologyCenterAdminRoleId);
                addedRadiologyCenterAdminRole.OwnershipId = radiologyCenterAdminUserId;

                var addedRadiologyCenterReceptionistRole = radiologyCenterSubUser.AddUserRole(radiologyCenterReceptionistRoleId);
                addedRadiologyCenterReceptionistRole.OwnershipId = radiologyCenterAdminUserId;


                /******************************* Assign Permissions To Users *********************************/

                foreach (var permission in radiologyCenterReceptionistRole.Permissions)
                {
                    var addedPermission = addedRadiologyCenterAdminRole.AddPermission(permission.Id);
                    addedPermission.OwnershipId = radiologyCenterAdminUserId;
                }

                /******************************* Defined Identity Users *********************************/

                var IdentitySuperAdminUser = new ApplicationUser
                {
                    Id = superAdminUserId.Value,
                    UserName = superAdminUser.UserName,
                    Email = superAdminUser.Email,
                    PhoneNumber = superAdminUser.Mobile,
                };

                var identityRadiologyCenterAdminUser = new ApplicationUser
                {
                    Id = radiologyCenterAdminUserId.Value,
                    UserName = radiologyCenterAdminUser.UserName,
                    Email = radiologyCenterAdminUser.Email,
                    PhoneNumber = radiologyCenterAdminUser.Mobile,
                };

                var identityRadiologyCenterSubUser = new ApplicationUser
                {
                    Id = radiologyCenterSubUserId.Value,
                    UserName = radiologyCenterSubUser.UserName,
                    Email = radiologyCenterSubUser.Email,
                    PhoneNumber = radiologyCenterSubUser.Mobile,
                };

                /******************************* Inserting Data Into DataBase *********************************/

                await dbContext.Permissions.AddAsync(permission_1);
                await dbContext.Permissions.AddAsync(permission_2);
                await dbContext.Permissions.AddAsync(permission_3);
                await dbContext.Permissions.AddAsync(permission_4);
                await dbContext.Permissions.AddAsync(permission_5);
                await dbContext.Permissions.AddAsync(permission_6);

                await dbContext.Roles.AddAsync(superAdminRole);
                await dbContext.Roles.AddAsync(radiologyCenterAdminRole);
                await dbContext.Roles.AddAsync(radiologyCenterReceptionistRole);
                await dbContext.Roles.AddAsync(radiologyCenterRadiologistRole);
                await dbContext.Roles.AddAsync(radiologyCenterAccountantRole);

                await dbContext.SystemUsers.AddAsync(superAdminUser);
                await dbContext.SystemUsers.AddAsync(radiologyCenterAdminUser);
                await dbContext.SystemUsers.AddAsync(radiologyCenterSubUser);

                await userManager.CreateAsync(IdentitySuperAdminUser, "010011012");
                await userManager.CreateAsync(identityRadiologyCenterAdminUser, "011010012");
                await userManager.CreateAsync(identityRadiologyCenterSubUser, "012011010");

                await dbContext.SaveChangesAsync();

                dbContext.IsSeeding = false;
            }
        }
    }
}
