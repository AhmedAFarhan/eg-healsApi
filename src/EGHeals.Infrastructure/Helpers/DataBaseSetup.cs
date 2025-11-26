using EGHeals.Domain.Enums.Shared;
using EGHeals.Domain.Models.Shared.Users;
using EGHeals.Domain.ValueObjects.Shared.Users;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Extensions;
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

                #region PERMISSIONS

                var superAdminPermissionId = PermissionId.Of(Guid.NewGuid());
                var superAdminPermission = Permission.Create(superAdminPermissionId, "Super Admin", UserActivity.SYSTEM, true);

                var radiologyCenter_AdminPermissionId = PermissionId.Of(Guid.NewGuid());
                var radiologyCenter_AdminPermission = Permission.Create(radiologyCenter_AdminPermissionId, "Radiology Center Admin", UserActivity.RADIOLOGY, true);

                var radiologyCenter_AddEncounterPermissionId = PermissionId.Of(Guid.NewGuid());
                var radiologyCenter_AddEncounterPermission = Permission.Create(radiologyCenter_AddEncounterPermissionId, "Add Encounter", UserActivity.RADIOLOGY);

                var radiologyCenter_UpdateEncounterPermissionId = PermissionId.Of(Guid.NewGuid());
                var radiologyCenter_UpdateEncounterPermission = Permission.Create(radiologyCenter_UpdateEncounterPermissionId, "Update Encounter", UserActivity.RADIOLOGY);

                var radiologyCenter_DeleteEncounterPermissionId = PermissionId.Of(Guid.NewGuid());
                var radiologyCenter_DeleteEncounterPermission = Permission.Create(radiologyCenter_DeleteEncounterPermissionId, "Delete Encounter", UserActivity.RADIOLOGY);

                var radiologyCenter_ReadEncountersPermissionId = PermissionId.Of(Guid.NewGuid());
                var radiologyCenter_ReadEncountersPermission = Permission.Create(radiologyCenter_ReadEncountersPermissionId, "Read Encounters", UserActivity.RADIOLOGY);

                #endregion

                #region TENANTS

                var superAdminTenantId = TenantId.Of(Guid.NewGuid());
                var superAdminTenant = Tenant.Create(superAdminTenantId, "98741258963258", Gender.MALE, UserActivity.SYSTEM, "some description");

                var radiologyCenetrTenant1Id = TenantId.Of(Guid.NewGuid());
                var radiologyCenetrTenant1 = Tenant.Create(radiologyCenetrTenant1Id, "12345678912365", Gender.MALE, UserActivity.RADIOLOGY, "some description");

                #endregion

                #region ROLES

                var superAdminRoleId = RoleId.Of(Guid.NewGuid());
                var superAdminRole = Role.Create(superAdminRoleId, "Super Admin", true);
                superAdminRole.TenantId = superAdminTenantId;
                var superAdminRolePermission = superAdminRole.AddPermission(superAdminPermissionId);
                superAdminRolePermission.TenantId = superAdminTenantId;

                var radiologyCenter_AdminRoleId = RoleId.Of(Guid.NewGuid());
                var radiologyCenter_AdminRole = Role.Create(radiologyCenter_AdminRoleId, "Radiology Center Admin Role");
                radiologyCenter_AdminRole.TenantId = radiologyCenetrTenant1Id;
                var radiologyCenter_AdminRolePermission = radiologyCenter_AdminRole.AddPermission(radiologyCenter_AdminPermissionId);
                radiologyCenter_AdminRolePermission.TenantId = radiologyCenetrTenant1Id;

                var radiologyCenter_ReceptionistRoleId = RoleId.Of(Guid.NewGuid());
                var radiologyCenter_ReceptionistRole = Role.Create(radiologyCenter_ReceptionistRoleId, "Receptionist");
                radiologyCenter_ReceptionistRole.TenantId = radiologyCenetrTenant1Id;

                var radiologyCenter_ReceptionistRoleAddPermission = radiologyCenter_ReceptionistRole.AddPermission(radiologyCenter_AddEncounterPermissionId);
                radiologyCenter_ReceptionistRoleAddPermission.TenantId = radiologyCenetrTenant1Id;

                var radiologyCenter_ReceptionistRoleUpdatePermission = radiologyCenter_ReceptionistRole.AddPermission(radiologyCenter_UpdateEncounterPermissionId);
                radiologyCenter_ReceptionistRoleUpdatePermission.TenantId = radiologyCenetrTenant1Id;

                var radiologyCenter_ReceptionistRoleDeletePermission = radiologyCenter_ReceptionistRole.AddPermission(radiologyCenter_DeleteEncounterPermissionId);
                radiologyCenter_ReceptionistRoleDeletePermission.TenantId = radiologyCenetrTenant1Id;

                var radiologyCenter_ReceptionistRoleReadPermission = radiologyCenter_ReceptionistRole.AddPermission(radiologyCenter_ReadEncountersPermissionId);
                radiologyCenter_ReceptionistRoleReadPermission.TenantId = radiologyCenetrTenant1Id;

                #endregion

                #region USERS

                var superAdminUserId = UserId.Of(Guid.NewGuid());
                var superAdminUser = User.Create(superAdminUserId, "Super Admin", "Super Admin", "super@super.com", "01099315900");
                superAdminUser.TenantId = superAdminTenantId;
                var superAdminUserRole = superAdminUser.AddRole(superAdminRoleId);
                superAdminUserRole.TenantId = superAdminTenantId;

                var radiologyCenter_AdminUser1Id = UserId.Of(Guid.NewGuid());
                var radiologyCenter_AdminUser1 = User.Create(radiologyCenter_AdminUser1Id, "Radiology Admin 1", "Radiology Admin 1", "radiology1@radiology.com", "01099315900");
                radiologyCenter_AdminUser1.TenantId = radiologyCenetrTenant1Id;
                var radiologyCenter_Admin1UserRole = radiologyCenter_AdminUser1.AddRole(radiologyCenter_AdminRoleId);
                radiologyCenter_Admin1UserRole.TenantId = radiologyCenetrTenant1Id;

                var radiologyCenter_Radiology1_SubUser1Id = UserId.Of(Guid.NewGuid());
                var radiologyCenter_Radiology1_SubUser1 = User.Create(radiologyCenter_Radiology1_SubUser1Id, "first name", "last name", "sub1@sub.com", "01017472751");
                radiologyCenter_Radiology1_SubUser1.TenantId = radiologyCenetrTenant1Id;
                var radiologyCenter_ReceptionistUser1Role = radiologyCenter_Radiology1_SubUser1.AddRole(radiologyCenter_ReceptionistRoleId);
                radiologyCenter_ReceptionistUser1Role.TenantId = radiologyCenetrTenant1Id;

                var radiologyCenter_Radiology1_SubUser2Id = UserId.Of(Guid.NewGuid());
                var radiologyCenter_Radiology1_SubUser2 = User.Create(radiologyCenter_Radiology1_SubUser2Id, "first name", "last name", "sub2@sub.com", "01017472752");
                radiologyCenter_Radiology1_SubUser2.TenantId = radiologyCenetrTenant1Id;
                var radiologyCenter_ReceptionistUser2Role = radiologyCenter_Radiology1_SubUser2.AddRole(radiologyCenter_ReceptionistRoleId);
                radiologyCenter_ReceptionistUser2Role.TenantId = radiologyCenetrTenant1Id;

                /******************************* Defined Identity Users *********************************/

                var IdentitySuperAdminUser = superAdminUser.ToIdentityUser();

                var identityRadiologyCenter_AdminUser1 = radiologyCenter_AdminUser1.ToIdentityUser();

                var identityRadiologyCenter_Radiology1_SubUser1 = radiologyCenter_Radiology1_SubUser1.ToIdentityUser();

                var identityRadiologyCenter_Radiology1_SubUser2 = radiologyCenter_Radiology1_SubUser2.ToIdentityUser();

                #endregion

                #region INSERTING

                await dbContext.Permissions.AddAsync(superAdminPermission);
                await dbContext.Permissions.AddAsync(radiologyCenter_AdminPermission);
                await dbContext.Permissions.AddAsync(radiologyCenter_AddEncounterPermission);
                await dbContext.Permissions.AddAsync(radiologyCenter_UpdateEncounterPermission);
                await dbContext.Permissions.AddAsync(radiologyCenter_ReadEncountersPermission);
                await dbContext.Permissions.AddAsync(radiologyCenter_DeleteEncounterPermission);

                await dbContext.Roles.AddAsync(superAdminRole);
                await dbContext.Roles.AddAsync(radiologyCenter_AdminRole);
                await dbContext.Roles.AddAsync(radiologyCenter_ReceptionistRole);

                await dbContext.Tenants.AddAsync(superAdminTenant);
                await dbContext.Tenants.AddAsync(radiologyCenetrTenant1);

                await userManager.CreateAsync(IdentitySuperAdminUser, "010011012");
                await userManager.CreateAsync(identityRadiologyCenter_AdminUser1, "011010012");
                await userManager.CreateAsync(identityRadiologyCenter_Radiology1_SubUser1, "011010012");
                await userManager.CreateAsync(identityRadiologyCenter_Radiology1_SubUser2, "012011010");

                #endregion

                await dbContext.SaveChangesAsync();

                dbContext.IsSeeding = false;
            }
        }
    }
}
