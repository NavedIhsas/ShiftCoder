using System;
using System.IO;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _0_FrameWork.Application;
using _0_Framework.Application.ZarinPal;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Infrastructure;
using AspNetCore.SEOHelper;
using BlogManagement.Infrastructure;
using CommentManagement.Infrastructure;
using DiscountManagement.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using ShopManagement.Configuration.Permission;
using WebHost.Permissions;

namespace WebHost
{
    public class Startup
    { // ReSharper disable CommentTypo
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddMvcOptions(option => option.Filters.Add<SecurityPageFilter>());
            services.AddHttpContextAccessor();

            services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/Sitemap", "Sitemap.xml");
            });

            #region IOC
            var connectionString = Configuration.GetConnectionString("ShiftCoderConnection");
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IEpisodeFileUploader, EpisodeUploadFile>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
            services.AddTransient<IRazorPartialToStringRenderer, RazorPartialToStringRenderer>();
            services.AddTransient<IPermissionExposer, WebHostPermissionExposer>();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            ShopManagementBootstrapper.Configure(services, connectionString);
            BlogManagementBootstrapper.Configure(services, connectionString);
            CommentManagementBootstrapper.Configure(services, connectionString);
            DiscountManagementBootstrapper.Configure(services, connectionString);
            AccountManagementBootstrapper.Configure(services, connectionString);
            #endregion

            #region Authentication

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    var context = new HttpContextAccessor();

                    o.LoginPath = new PathString("/Login");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
                });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/robots.txt"))
                {
                    var robotsTxtPath = Path.Combine(env.ContentRootPath, "robots.txt");
                    string output = "User-agent: *  \nallow: /";
                    if (File.Exists(robotsTxtPath))
                    {
                        output = await File.ReadAllTextAsync(robotsTxtPath);
                    }
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(output);
                }
                else await next();
            });


           

            //  این میدیلویر برای چک کردن اینه که کاربر نتواند با استفاده از یوآرال به فایل ها  دسترسی داشته باشد
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path.Value != null &&
            //        context.Request.Path.Value.ToString().ToLower().StartsWith("/fileuploader"))
            //    {
            //        var callingUrl = context.Request.Headers["Referer"].ToString();

            //        if (callingUrl != "" && (callingUrl.StartsWith("https://ihsasdevelopment.ir/") ||
            //                                 callingUrl.StartsWith("http://ihsasdevelopment.ir/")))
            //        {
            //            await next.Invoke();
            //        } 
            //        else
            //        {
            //            context.Response.Redirect("/Login");
            //        }

            //    }
            //    else
            //    {
            //        await next.Invoke();
            //    }


            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseXMLSitemap(env.ContentRootPath);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
