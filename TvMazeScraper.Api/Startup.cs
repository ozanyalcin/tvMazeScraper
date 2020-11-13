using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TvMazeScraper.Api.DataInitilization;
using TvMazeScraper.Api.Repository;
using TvMazeScraper.Api.Services;
using TvMazeScraper.Api.Settings;

namespace TvMazeScraper.Api
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
			services.AddControllers();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "TVShowsManagementAPI", Version = "v1", });
			});

			services.AddHostedService<MigratorHostedService>();
			services.AddSingleton<ITvMazeApiIntegrationService, TvMazeApiIntegrationService>();
			services.AddSingleton<IShowsRepository, ShowsRepository>();
			services.AddSingleton<IShowsManagementService, ShowsManagementService>();
			services.AddSingleton<ITvMazeDataInitializeService, TvMazeDataInitializeService>();

			services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
			services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);

			services.AddHttpClient();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "TVShowsManagementAPI");
			});
		}
	}
}
