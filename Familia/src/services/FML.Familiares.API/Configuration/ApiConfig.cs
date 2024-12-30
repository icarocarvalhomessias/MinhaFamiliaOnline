using FML.Familiares.API.Data;
using FML.WebApi.Core.Identidade;
using Microsoft.EntityFrameworkCore;

namespace FML.Familiares.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FamiliaresContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddJwtConfiguration(configuration);
            services.AddSwaggerConfiguration();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApiConfig).Assembly));
            services.AddHttpContextAccessor();
            services.RegisterServices(configuration);
            services.RegisterJson();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Total");
            app.UseAuthConfiguration();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
