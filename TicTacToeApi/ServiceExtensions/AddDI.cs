using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace TicTacToeApi.ServiceExtensions
{
    public static class AddDI
    {
        public static IServiceCollection AddDIConf(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IGamePlayerJunctionRepository, GamePlayerJunctionRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IFieldService, FieldService>();

            services.AddScoped<IPlayerService, PlayerService>();

            return services;
        }
    }
}
