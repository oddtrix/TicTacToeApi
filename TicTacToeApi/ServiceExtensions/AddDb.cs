using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Contexts;

namespace TicTacToeApi.ServiceExtensions
{
    public static class AddDb
    {
        public static IServiceCollection AddDbConf(this IServiceCollection services, string domainConnectionString, string identityConnectionString)
        {
            services.AddDbContext<AppDomainContext>(options =>
            {
                options.UseSqlServer(domainConnectionString);
            });

            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(identityConnectionString);
            });

            services.AddIdentity<AppIdentityUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            return services;
        }
    }
}
