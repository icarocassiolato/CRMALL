using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientesAPI.Banco;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using ClientesAPI.Repositorios;

namespace ClientesAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextoBD>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<ICliente, Cliente>();
            services.AddMvcCore();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                                  });
            });

            // services.AddResponseCaching();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("CRMALL eh demais! :D");
                });

                endpoints.MapControllers();
            });
        }
    }
}
