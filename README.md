### 项目简介

###### LayIM.AspNetCore 是基于ASP.NET CORE Middleware 对前端WebIM框架LayIM后端的一个深度封装。目标是通过简单配置即可使用，并且用户可以自定义实现。
###### LayIM官网地址：http://layim.layui.com/
-- 本人非LayIM作者，LayIM 销售权，著作权请到官网了解详情。
-- 数据库为SQL Server 2008，开发工具为Visual Studio 2017 开发环境：.NET CORE2.1

###### 快速开始
---
 第一步：执行项目中的脚本 ：layim.sqlserver.createdb layim.sqlserver.initdata
 第二步：修改Demo项目中的配置参数，由于是提供了融云的默认实现，所以目前需要配置融云参数。
```c#
  public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //注册LayIM的默认服务
            services.AddLayIM()
                //使用融云通信（如果自定义的话，这里改成自定义的即可。需要实现 ILayIMServer接口）
                .AddRongCloud(config =>
                    {
                        config.AppKey = "pvxdm17jpv1or";
                        config.AppSecret = "I8a4qFGzFe8";
                    })
                 //使用SqlServer保存相关信息
                .AddSqlServer("server=192.168.1.18;user id=sa;password=123123;database=LayIM;Min Pool Size=16;Connect Timeout=500;");

            services.AddSession();
        }
```
``` c#
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
	    ..其他代码
            //客户端模拟使用session保存当前用户ID
            app.UseSession();

            //使用LayIM，自定义配置
            app.UseLayIM(options => {
                //默认前缀就是/layim，不过也不推荐自定义（可能有bug。。。）
                //options.ApiPrefix = "/layim";
                options.UserFactory = new MyUserFactory();
            });

	    ..其他代码
        }
```

---
运行项目即可，默认进入account/?uid=10001页面，其他浏览器可以使用此路径模拟登录。

--- 
项目运行图在images文件夹中。部分图示例：

![](http://img1.gurucv.com/image/2018/6/2/9eb97dc360cb42fca59c757e6fa2511c.png)
![](http://img1.gurucv.com/image/2018/6/2/ce83babb67dd4a99aa700033e2b59765.png)
