## LayIM.AspNetCore Middleware

一个基于AspNetCore的中间件。对LayIM的功能实现做了深度封装。达到配置即可用。默认提供了融云的实现和SqlServer的存储实现。后期会加上SignalR的实现。

LayIM官网地址：http://layim.layui.com/

### 1 执行项目中的脚本 ：layim.sqlserver.createdb layim.sqlserver.initdata
### 2 修改Demo项目中的配置参数，由于是提供了融云的默认实现，所以目前需要配置融云参数。
```c#
  public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //注册LayIM的默认服务
            services.AddLayIM()
                //使用融云通信（如果自定义的话，这里改成自定义的即可。需要实现 ILayIMServer接口）
                .AddRongCloud(config =>
                    {
                        config.AppKey = "******";
                        config.AppSecret = "******";
                    })
                 //使用SqlServer保存相关信息
                .AddSqlServer("******");

            services.AddSession();
        }
```
---
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
