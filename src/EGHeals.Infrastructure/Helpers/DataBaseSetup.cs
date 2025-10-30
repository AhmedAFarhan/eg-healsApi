using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Extensions;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Infrastructure.Helpers
{
    public class DataBaseSetup(ApplicationIdentityDbContext dbContext, UserManager<AppUser> userManager)
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

            // Seed data if database is empty
            var existRoles = await dbContext.Roles.AnyAsync();

            if (!existRoles)
            {
                dbContext.IsSeeding = true;

                /******************************* Defined Permissions *********************************/

                var superAdminPermissionId = PermissionId.Of(Guid.NewGuid());
                var superAdminPermission = Permission.Create(superAdminPermissionId, "Super Admin Permission");

                var radiologyCenterAdminPermissionId = PermissionId.Of(Guid.NewGuid());
                var radiologyCenterAdminPermission = Permission.Create(radiologyCenterAdminPermissionId, "Radiology Center Admin Permission");

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


                /******************************* Defined Roles and assign permissions to it *********************************/

                var superAdminRoleId = RoleId.Of(Guid.NewGuid());
                var superAdminRole = Role.Create(superAdminRoleId, "SuperAdmin", null, true);

                superAdminRole.AddPermission(superAdminPermissionId);

                var radiologyCenterAdminRoleId = RoleId.Of(Guid.NewGuid());
                var radiologyCenterAdminRole = Role.Create(radiologyCenterAdminRoleId, "RadiologyCenterAdmin", UserActivity.RADIOLOGY, true);

                radiologyCenterAdminRole.AddPermission(radiologyCenterAdminPermissionId);

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


                /******************************* Defined Domain Users *********************************/

                var superAdminUserId = UserId.Of(Guid.NewGuid());
                var superAdminUser = User.Create(superAdminUserId, "first name", "last name", "super@super.com", "01099315900");
                superAdminUser.OwnershipId = superAdminUserId;

                var radiologyCenterAdminUserId = UserId.Of(Guid.NewGuid());
                var radiologyCenterAdminUser = User.Create(radiologyCenterAdminUserId, "first name", "last name", "radiology@radiology.com", "01096513165");
                radiologyCenterAdminUser.OwnershipId = radiologyCenterAdminUserId;

                var radiologyCenterSubUserId = UserId.Of(Guid.NewGuid());
                var radiologyCenterSubUser = User.Create(radiologyCenterSubUserId, "first name", "last name", "sub@sub.com", "01093266551");
                radiologyCenterSubUser.OwnershipId = radiologyCenterAdminUserId;

                /******************************* Assign Role Permissions To Users *********************************/

                var superAdminUserRolePermission = superAdminUser.AddPermission(superAdminPermissionId);
                superAdminUserRolePermission.OwnershipId = superAdminUserId;

                var radiologyCenterAdminUserRolePermission = radiologyCenterAdminUser.AddPermission(radiologyCenterAdminPermissionId);
                radiologyCenterAdminUserRolePermission.OwnershipId = radiologyCenterAdminUserId;

                var radiologyCenterSubUserRolePermission_1 = radiologyCenterSubUser.AddPermission(permissionId_1);
                radiologyCenterSubUserRolePermission_1.OwnershipId = radiologyCenterAdminUserId;
                var radiologyCenterSubUserRolePermission_2 = radiologyCenterSubUser.AddPermission(permissionId_2);
                radiologyCenterSubUserRolePermission_2.OwnershipId = radiologyCenterAdminUserId;

                /******************************* Defined Identity Users *********************************/

                var IdentitySuperAdminUser = superAdminUser.ToIdentityUser();

                var identityRadiologyCenterAdminUser = radiologyCenterAdminUser.ToIdentityUser();

                var identityRadiologyCenterSubUser = radiologyCenterSubUser.ToIdentityUser();

                /******************************* Inserting Data Into DataBase *********************************/

                await dbContext.Permissions.AddAsync(superAdminPermission);
                await dbContext.Permissions.AddAsync(radiologyCenterAdminPermission);
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

                await userManager.CreateAsync(IdentitySuperAdminUser, "010011012");
                await userManager.CreateAsync(identityRadiologyCenterAdminUser, "011010012");
                await userManager.CreateAsync(identityRadiologyCenterSubUser, "012011010");

                await dbContext.SaveChangesAsync();

                dbContext.IsSeeding = false;
            }
        }
    }
}
