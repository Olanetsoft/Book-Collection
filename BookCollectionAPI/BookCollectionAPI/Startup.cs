using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCollectionAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookCollectionAPI
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = Configuration["connectionStrings:bookDbConnectionString"];
            services.AddDbContext<BookDBContext>(c => c.UseSqlServer(connectionString));

            // service for country
            services.AddScoped<ICountryRepository, CountryRepository>();

            // service for category
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();

            // service for reviewer
            services.AddScoped<IReviewerRepository, ReviewerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BookDBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            // Used to seed data to the database 
            // Its commented out to avoid seeding each time the app is satrted
            // context.SeedDataContext();

            app.UseMvc();
        }
    }
}
