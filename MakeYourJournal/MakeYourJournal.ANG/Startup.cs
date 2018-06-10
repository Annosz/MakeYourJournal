using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MakeYourJournal.ANG.Middlewares;
using MakeYourJournal.ANG.Services;
using MakeYourJournal.DAL;
using MakeYourJournal.DAL.Entities;
using MakeYourJournal.DAL.Mapping;
using MakeYourJournal.DAL.Seed;
using MakeYourJournal.DAL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MakeYourJournal.ANG
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
            services.AddDbContext<JournalDbContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString(nameof(JournalDbContext)))
                .UseLoggerFactory(new LoggerFactory().AddDebug())
            );
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                 .AddEntityFrameworkStores<JournalDbContext>()
                 .AddDefaultTokenProviders();
            services.AddTransient<JournalDataSeeder>();
            services.AddTransient<JournalSeedData>();

            services.AddSingleton<IMapper>(MapperConfig.Configure());
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<INoteService, NoteService>();

            services.AddMvc();

            services.Configure<AuthMessageSenderOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseWhen(
                 ctx => ctx.Request.Path.HasValue && ctx.Request.Path.StartsWithSegments(new PathString("/api")),
                 b => b.UseMiddleware<ApiExceptionHandlerMiddleware>()
            );


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
