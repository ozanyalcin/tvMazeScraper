using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TvMazeScraper.Api.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.DataInitilization
{
	public class MigratorHostedService : IHostedService
	{
		private readonly IServiceProvider _serviceProvider;
		public MigratorHostedService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			var _tvMazeDataInitiliazeService = scope.ServiceProvider.GetRequiredService<ITvMazeDataInitializeService>();
			await _tvMazeDataInitiliazeService.InitializeData();
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
	}
}
