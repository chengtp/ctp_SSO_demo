using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSOProjectOne.Models;

namespace SSOProjectOne
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //两种方式读取 
            // var defaultcon = Configuration.GetConnectionString("DefaultConnection");
            StaticParameter.ValidateToken = Configuration["StaticParameter:ValidateToken"];
            StaticParameter.KeepToken = Configuration["StaticParameter:KeepToken"];
            services.AddMvc();
            services.AddAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //在控制台中输出log
            loggerFactory.AddConsole();

            //在开发环境下，显示错误详细信息
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //允许访问wwwroot文件夹下的静态文件
            app.UseStaticFiles();

            //设置身份验证方式
            // app.UseIdentity();
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                // Cookie 验证方案名称，后面多处都需要用到，部分地方必须要求常量，所以直接配置为字符串。
                AuthenticationScheme = "Cookie",
                LoginPath = new PathString("/Home/Login"),  // 登录地址
                AccessDeniedPath = new PathString("/Home/Forbidden"),
                // 是否自动启用验证，如果不启用，则即便客服端传输了Cookie信息，服务端也不会主动解析。
                // 除了明确配置了 [Authorize(ActiveAuthenticationSchemes = "上面的方案名")] 属性的地方，才会解析，此功能一般用在需要在同一应用中启用多种验证方案的时候。比如分Area.
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            // 设置MVC路由
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
