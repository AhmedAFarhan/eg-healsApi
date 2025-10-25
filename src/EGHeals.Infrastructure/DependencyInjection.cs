using BuildingBlocks.DataAccess.UnitOfWork;
using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using EGHeals.Application.Contracts.Roles;
using EGHeals.Application.Contracts.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Data.Interceptors;
using EGHeals.Infrastructure.Helpers;
using EGHeals.Infrastructure.Identity;
using EGHeals.Infrastructure.Repositories.Roles;
using EGHeals.Infrastructure.Repositories.Users;
using EGHeals.Infrastructure.Services;
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
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>(sp =>
            {
                return new AuditableEntityInterceptor(() => sp.GetRequiredService<ICurrentUserService>());
            });

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("Database"));
            });

            services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));

            // Register Data Protection (required for token providers)
            services.AddDataProtection();

            // Register Identity
            services.AddIdentityCore<ApplicationUser>(options =>
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

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddTransient<DataBaseSetup>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

            services.AddScoped<IUserRepository>(sp =>
            {
                var db = sp.GetRequiredService<ApplicationDbContext>();
                var userContext = sp.GetRequiredService<ICurrentUserService>();
                return new UserRepository<ApplicationDbContext>(db, userContext);
            });

            services.AddScoped<IRoleRepository>(sp =>
            {
                var db = sp.GetRequiredService<ApplicationDbContext>();
                var userContext = sp.GetRequiredService<ICurrentUserService>();
                return new RoleRepository<ApplicationDbContext>(db, userContext);
            });

            return services;
        }
    }
}
