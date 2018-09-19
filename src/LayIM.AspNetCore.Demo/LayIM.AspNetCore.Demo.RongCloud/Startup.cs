using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LayIM.AspNetCore.Core;
using LayIM.AspNetCore.Demo.RongCloud.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LayIM.AspNetCore.Demo.RongCloud
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //注册LayIM的默认服务
            services.AddLayIM(() =>
            {
                return new MyUserFactory();
            }).AddRongCloud(config =>
                    {
                        config.AppKey = "pvxdm17jpv1or";
                        config.AppSecret = "I8a4qFGzFe8";
                    }).AddSqlServer("server=DESKTOP-GK56MO8\\PZSQLSERVER;user id=sa;password=panzi123;database=LayIM;Min Pool Size=16;Connect Timeout=500;");
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSession();

            //使用LayIM，自定义配置
            app.UseLayIM(options =>
            {
                options.ServerType = ServerType.RongCloud;
            });
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
