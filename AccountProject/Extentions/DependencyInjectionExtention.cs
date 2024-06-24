using AccountProject.ApplicationLayer.Interfaces;
using AccountProject.ApplicationLayer.Services;
using AccountProject.InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountProject.Extentions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<TestDevContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TestDev")));
            services.AddScoped<IAccountService, AccountService>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }
}
