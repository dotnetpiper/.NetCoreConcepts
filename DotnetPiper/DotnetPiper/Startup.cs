using DotnetPiper.Filters;
using DotnetPiper.Filters.ActionFilters;
using DotnetPiper.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace DotnetPiper
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            services.AddMvc();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IFirstService, FirstService>();
            services.AddScoped<ArticlesService>();
            //services.AddTransient<ArticlesService>();

            services.AddScoped<CustomLogActionOneFilter>();       
            services.AddScoped<CustomOneLoggingExceptionFilter>();
            services.AddScoped<CustomOneResourceFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IFirstService FirstService)
        {

            loggerFactory.AddConsole();
            app.UseRewriter(new RewriteOptions()
            //.AddRedirect("redirect-rule/(.*)", "redirected/$1")
            //.AddRedirect(@"product/Create/(\d+)", "product/Details?id=$1")
            .AddRewrite(@"product/Create/(\d+)", "product/Details?id=$1", skipRemainingRules: true)
            //.AddRewrite(@"^rewrite-rule/(\d+)", "rewritten?var1=$1&var2=$2", skipRemainingRules: false)
            .Add(new RedirectImageRequests(".png", "/png-images")));
            //.Add(new RedirectImageRequests(".jpg", "/jpg-images")));



            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
            });
            //app.Use(async (context, next) =>
            //{
            //    string agent = context.Request.Headers["user-agent"].ToString();

            //    if (!string.IsNullOrEmpty(context.Request.QueryString.Value))
            //    {
            //        context.Response.Headers.Add("X-Frame-Options", "localhost");
            //        //context.Response.Headers.Add("X-Content-Type-Options", configuration["CustomMessage"]);
            //        await context.Response.WriteAsync("Query string is not allowed in Middleware pipeline");
            //        await next.Invoke();
            //    }
            //});

            app.Map("/map1", HandleMapMiddlewareOne);

            app.Map("/map2", HandleMapMiddlewareTwo);

            //app.MapWhen(context => context.Request.QueryString.Value.Equals("DeviceId"), appBuilder => 
            //{
            //    app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Response for devices");

            //    });
            //});

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"),HandleBranch);
           
            //Commented for testing purpose only
            app.UseSecurityMiddleware();

            app.Run(async (context) =>
            {
                //throw new Exception("From Middle ware");
                //string welcomeMsg = Configuration["WelcomeEquinox"];
                string welcomeMsg = FirstService.WelcomeEquinox();
                await context.Response.WriteAsync(welcomeMsg);
                //await context.Response.WriteAsync("welcomeMsg");
            });
        }

        private static void HandleMapMiddlewareOne(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                 await context.Response.WriteAsync("Map Middleware One Called...");
               
            });
        }

        private static void HandleMapMiddlewareTwo(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Middleware Two Called...");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }
    }
}
