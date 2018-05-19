using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MakeYourJournal.DAL;
using MakeYourJournal.DAL.Mapping;
using MakeYourJournal.DAL.Seed;
using MakeYourJournal.DAL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MakeYourJournal.API
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
            services.AddTransient<JournalDataSeeder>();
            services.AddTransient<JournalSeedData>();

            services.AddSingleton<IMapper>(MapperConfig.Configure());

            services.AddScoped<IIssueService,IssueService>();
            services.AddScoped<ITodoService,TodoService>();
            services.AddScoped<IArticleService, ArticleService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
