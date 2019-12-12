using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CUSTOMERAPISQL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using System.Web.Http.Cors;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;

namespace CUSTOMERAPISQL
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
            // services.AddIdentity<IdentityUser, IdentityRole>();
            services.AddControllersWithViews();
            IdentityModelEventSource.ShowPII = true;
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //        .Build();
            //    });
            //});
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(token =>
        {
            token.RequireHttpsMetadata = false;
            token.SaveToken = true;
            token.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
            //Same Secret key will be used while creating the token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                ValidateIssuer = true,
            //Usually, this is your application base URL
            ValidIssuer = Configuration["Jwt:Issuer"],
                ValidateAudience = true,

                ValidAudience = Configuration["Jwt:Issuer"],
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
            services.AddMvc();
            services.AddAuthentication();
            services.AddAuthentication(options => options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            //    options =>
            //    {
            //        options.LoginPath = new PathString("/Signin-google");
            //        options.AccessDeniedPath = new PathString("/auth/denied");
            //    });
            services.AddAuthentication().AddGoogle(options =>
        {
            IConfigurationSection googleAuthNSection =
                Configuration.GetSection("Authentication:Google");

            options.ClientId = googleAuthNSection["ClientId"];
            options.ClientSecret = googleAuthNSection["ClientSecret"];
            options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents()
            {
                OnRemoteFailure = LoginFailureHandler =>
                {
                    var authProperties = options.StateDataFormat.Unprotect(LoginFailureHandler.Request.Query["state"]);
                    LoginFailureHandler.Response.Redirect("/Identity/Account/Login");
                    return Task.FromResult(0);
                }
            };
        });
            //services.AddAuthentication(options => options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            //           services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});
            services.AddControllers();
            //services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<PersonDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("PersonDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();
          
          
            app.UseAuthorization();
            app.UseAuthentication();
            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller:Customer1}/{action:index}/{id?}");
                endpoints.MapControllers();
            });
           
        }
    }
}
