using BuildingBlocks.DataAccess.UnitOfWork;
using BuildingBlocks.DataAccessAbstraction.Services;
using BuildingBlocks.DataAccessAbstraction.UnitOfWork;
using BuildingBlocks.Responses.Factories;
using EGHeals.Application.Repositories.Users.EGHeals.Application.Contracts.Users;
using EGHeals.Application.Services.Jwt;
using EGHeals.Infrastructure.Authorization;
using EGHeals.Infrastructure.Data;
using EGHeals.Infrastructure.Data.Interceptors;
using EGHeals.Infrastructure.Helpers;
using EGHeals.Infrastructure.Repositories.Shared.Users;
using EGHeals.Infrastructure.Services.Jwt;
using EGHeals.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EGHeals.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            /************************************* Register Interceptor ***************************************/

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>(sp =>
            {
                return new AuditableEntityInterceptor(() => sp.GetRequiredService<IUserContextService>());
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.HttpContext.Items["AuthError"] = "Session expired. Please login again.";
                        }
                        else
                        {
                            context.HttpContext.Items["AuthError"] = "Invalid authentication token.";
                        }
                        
                        context.NoResult(); // Stop further exception propagation

                        return Task.CompletedTask;
                    },

                    OnChallenge = async context =>
                    {
                        context.HandleResponse(); // Skip default challenge behavior

                        var errorMessage = "You are not authorized to access this resource.";

                        if (context.HttpContext.Items.TryGetValue("AuthError", out var authError))
                        {
                            errorMessage = authError?.ToString() ?? errorMessage;
                        }

                        var response = EGResponseFactory.Fail<object>(
                            message: "Unauthorized",
                            errors: new List<string> { errorMessage },
                            traceId: context.HttpContext.TraceIdentifier,
                            code: StatusCodes.Status401Unauthorized
                        );

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(response);
                    }
                };
            });

            // Disable automatic claim type mapping
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, CustomClaimsPrincipalFactory>();

            services.AddHttpContextAccessor();

            services.AddScoped<IUserContextService, UserContextService>();

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));           

            /************************************* Register Repositories ***************************************/

            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationIdentityDbContext>>();

            services.AddScoped<IUserRepository, UserRepository>();

            //services.AddScoped<IRoleRepository, RoleRepository>();

            //services.AddScoped<IOwnerRepository, OwnerRepository>();

            //services.AddScoped<IOwnerRepository>(sp =>
            //{
            //    var db = sp.GetRequiredService<ApplicationIdentityDbContext>();
            //    var userContext = sp.GetRequiredService<ICurrentUserService>();
            //    return new OwnerRepository(db, userContext);
            //});

            /************************************* Register Services ************************************* **/

            services.AddScoped<IJwtService, JwtService>();

            //services.AddScoped<IUserQueryService, UserQueryService>();

            //services.AddScoped<IRadiologyExaminationCostQueryService, RadiologyExaminationCostQueryService>();


            return services;
        }
    }
}
