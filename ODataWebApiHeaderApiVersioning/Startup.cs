using ODataWebApiHeaderApiVersioning.Data;
using ODataWebApiHeaderApiVersioning.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace ODataWebApiHeaderApiVersioning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MoviesDbContext>(options => options.UseInMemoryDatabase(databaseName: "MoviesDb"));
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddMvc(
                options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            services.AddOData().EnableApiVersioning();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VersionedODataModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Filter().SkipToken();
                routeBuilder.MapVersionedODataRoutes("odata", "odata", modelBuilder.GetEdmModels());
            });

            AddSeedData(app);
        }

        private static void AddSeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetRequiredService<MoviesDbContext>();
                if (!db.Movies.Any())
                {
                    db.Movies.Add(new Movie { Name = "Bombshell", Classification = "R", RunningTime = 108 });
                    db.Movies.Add(new Movie { Name = "Ad Astra", Classification = "PG-13", RunningTime = 124 });
                    db.Movies.Add(new Movie { Name = "Schindler's List", Classification = "R", RunningTime = 195 });
                    db.Movies.Add(new Movie { Name = "Godzilla: King of the Monsters", Classification = "PG-13", RunningTime = 132 });
                    db.Movies.Add(new Movie { Name = "Ford v Ferrari", Classification = "PG-13", RunningTime = 152 });
                    db.Movies.Add(new Movie { Name = "Mad Max: Fury Road", Classification = "R", RunningTime = 120 });
                    db.Movies.Add(new Movie { Name = "Star Wars: The Rise of Skywalker", Classification = "PG-13", RunningTime = 143 });
                    db.Movies.Add(new Movie { Name = "Doctor Strange", Classification = "PG-13", RunningTime = 115 });
                    db.Movies.Add(new Movie { Name = "Dolittle", Classification = "PG", RunningTime = 101 });
                    db.Movies.Add(new Movie { Name = "Knives Out", Classification = "PG-13", RunningTime = 130 });
                    db.Movies.Add(new Movie { Name = "The Equalizer", Classification = "R", RunningTime = 132 });

                    db.SaveChanges();
                }
            }
        }
    }
}
