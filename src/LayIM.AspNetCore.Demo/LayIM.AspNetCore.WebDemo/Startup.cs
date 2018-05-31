using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LayIM.AspNetCore.WebDemo.User;

namespace LayIM.AspNetCore.WebDemo
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
            services.AddMvc();

            services.AddLayIM()
                .AddRongCloud(config =>
                    {
                        config.AppKey = "pvxdm17jpv1or";
                        config.AppSecret = "I8a4qFGzFe8";
                    })
                .AddSqlServer("server=192.168.1.18;user id=sa;password=123123;database=LayIM;Min Pool Size=16;Connect Timeout=500;");

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseHelloWorld();

            app.UseStaticFiles();
            //客户端模拟使用session保存当前用户ID
            app.UseSession();

            app.UseLayIM(options => {
                options.ApiPrefix = "/myapi";
                options.UserFactory = new MyUserFactory();
                options.UIConfig.UseUploadFile = false;
                options.UIConfig.UseUploadImage = false;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
