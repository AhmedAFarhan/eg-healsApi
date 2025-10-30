using BuildingBlocks.DataAccess.UnitOfWork;
using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Jwt;
using EGHeals.Application.Services.Roles;
using EGHeals.Application.Services.Users;
using EGHeals.Infrastructure.Authorization;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Data.Interceptors;
using EGHeals.Infrastructure.Helpers;
using EGHeals.Infrastructure.Repositories.Users;
using EGHeals.Infrastructure.Services.Jwt;
using EGHeals.Infrastructure.Services.Roles;
using EGHeals.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGHeals.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            /************************************* Register Interceptor ***************************************/

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>(sp =>
            {
                return new AuditableEntityInterceptor(() => sp.GetRequiredService<ICurrentUserService>());
            });

            /************************************* Register Database Context ***************************************/

            services.AddDbContext<ApplicationIdentityDbContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });

            /************************************* Register Data Seeding ***************************************/

            services.AddTransient<DataBaseSetup>();

            /************************************* Register Identity ***************************************/

            // Register Data Protection (required for token providers)
            services.AddDataProtection();

            // Register Identity
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddScoped<IJwtService, JwtService>();

            /************************************* Register Repositories ***************************************/

            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationIdentityDbContext>>();

            services.AddScoped<IUserRepository, UserRepository>();

            //services.AddScoped<IRoleRepository>(sp =>
            //{
            //    var db = sp.GetRequiredService<ApplicationIdentityDbContext>();
            //    var userContext = sp.GetRequiredService<ICurrentUserService>();
            //    return new RoleRepository<ApplicationIdentityDbContext>(db, userContext);
            //});

            /************************************* Register Services ***************************************/

            services.AddScoped<IUserQueryService, UserQueryService>();

            services.AddScoped<IRoleQueryService, RoleQueryService>();

            services.AddScoped<IUserCommandService, UserCommandService>();


            return services;
        }
    }
}
