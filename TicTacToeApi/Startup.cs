using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Domain.Identity;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TicTacToeApi.Contexts;
using TicTacToeApi.Hubs;
using TicTacToeApi.Models.AutoMapper;
using TicTacToeApi.ServiceExtensions;

namespace TicTacToeApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.UseMemberCasing();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerConf();

            string domainConnectionString = this.Configuration["ConnectionStrings:DomainDB"];
            string identityConnectionString = this.Configuration["ConnectionStrings:IdentityDB"];

            services.AddDbConf(domainConnectionString, identityConnectionString);

            services.AddSignalR(r => r.MaximumReceiveMessageSize = 5024000);

            string issuer = this.Configuration["JWT:Issuer"];
            string audience = this.Configuration["JWT:Audience"];
            string secretKey = this.Configuration["JWT:SecretKey"];

            services.AddJWTConf(issuer, audience, secretKey);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            services.AddAutoMapper(typeof(AutoMapperConfigProfile));

            services.AddDIConf();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment) 
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GameHub>("/game");
            });    
        }
    }
}
