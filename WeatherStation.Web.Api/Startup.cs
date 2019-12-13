using System;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherStation.Web.Api.Data;
using WeatherStation.Web.Api.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using WeatherStation.Web.Api.Models;
using SignalRChat.Hubs;
using WeatherStation.Web.Api.Helpers;


namespace WeatherStation.Web.Api
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")));


            services.AddSingleton<MeasurementService>();

            services.AddCors();
            services.AddMvc();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0); // Er denne nødvendig??
            services.AddSignalR();
            services.AddSingleton<counter>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("This is the best SecretKey Ever !should be moved out of here!")),

                        ValidateIssuer = true,
                        ValidIssuer = "WeatherStationApi",

                        ValidateAudience = true,
                        ValidAudience = "WeatherStation",

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            services.AddSingleton<IAccountService, AccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseWebSockets();

            app.UseRouting();

            app.UseAuthorization();




            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapHub<ChatHub>("/chatHub");
            });
            app.UseCors(builder =>
                     builder.WithOrigins("http://localhost:53613")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );

            app.UseAuthentication();
            //app.UseMvc();


        }
    }
}
