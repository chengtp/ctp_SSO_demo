using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SSOService
{
    public class Startup
    {
        //用来定义我们使用了哪些服务，比如MVC、EF、Identity、Logging、Route；也可以自定义一些服务。 这里定义的服务是基于依赖注入(Dependency Injection)的
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

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
