using Identity.Api.IdentityManagers;
using Identity.Api.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StructureMap;

namespace Identity.Api
{
    public class Program
    {
        private static IConfigurationRoot Configuration = default!;
        private static IContainer Container = default!;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Configuration = new ConfigurationBuilder().AddConfiguration(builder.Configuration).Build();
            Container = ApplicationFramework.BootstarpContainersForIdentityApi(Configuration);

            var prgm = new Program();

            prgm.ConfigureServices(builder.Services);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Container.Populate(services);

            services.TryAddScoped<IdentityCacheManager>();
            services.TryAddScoped<IdentitySecurityManager>();
            services.TryAddScoped<IdentityValidationManager>();

            services.AddScoped<Func<string, IIdentityManager>>(serviceProvider => key =>
             {
                 return key switch
                 {
                     "Cache" => serviceProvider.GetService<IdentityCacheManager>()!,
                     "Security" => serviceProvider.GetService<IdentitySecurityManager>()!,
                     "Validation" => serviceProvider.GetService<IdentitySecurityManager>()!,
                     _ => throw new NotImplementedException()
                 };
             });

            return Container.GetInstance<IServiceProvider>();
        }
    }
}
