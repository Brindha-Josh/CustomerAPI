using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerMgmt.Models;
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
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace CustomerMgmt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });

            //services.AddAuthentication(options =>
            //  {
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            //  })
            //  .AddCookie()
            //  .AddGoogle(options =>
            //  {
            //    options.ClientId = Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //  });

            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});
          
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationScheme = "ApplicationCookie",
            //    AutomaticAuthenticate = true
            //});

            //app.UseFacebookAuthentication(new FacebookOptions
            //{
            //    AuthenticationScheme = "Facebook",
            //    AppId = "YOUR_APP_ID",
            //    AppSecret = "YOUR_APP_SECRET",
            //    SignInScheme = "ApplicationCookie"
            //});

            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddDbContext<PersonDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PersonDB"))
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
