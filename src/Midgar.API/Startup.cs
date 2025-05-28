using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Midgar.Persistence.Context;
using Midgar.Persistence.Interfaces;
using Midgar.Persistence.Repositories;
using Midgar.ExternalServices.Interfaces;
using Midgar.ExternalServices.Services;

namespace Midgar.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
 
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MidgarContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
 
            services.AddHttpClient<IPokemonService, PokemonService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Midgar.API", Version = "v1" });
            });
        }
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Midgar.API v1"));
            }
 
            app.UseHttpsRedirection();
 
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(acess => acess.AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}