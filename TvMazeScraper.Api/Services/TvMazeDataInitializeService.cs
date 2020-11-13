using Polly;
using TvMazeScraper.Api.Domain;
using TvMazeScraper.Api.Dto;
using TvMazeScraper.Api.Models;
using TvMazeScraper.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Services
{
	public class TvMazeDataInitializeService : ITvMazeDataInitializeService
	{
		public readonly ITvMazeApiIntegrationService _tvMazeIntegrationService;
		public readonly IShowsRepository _showsRepository;

		public TvMazeDataInitializeService(
			ITvMazeApiIntegrationService tvMazeIntegrationService,
			IShowsRepository showsRepository)
		{
			_tvMazeIntegrationService = tvMazeIntegrationService;
			_showsRepository = showsRepository;
		}

		public async Task InitializeData()
		{
			var tvMazeShows = await GetTvMazeShows();

			var dbShows = new List<Show>();

			foreach (var tvMazeShow in tvMazeShows)
			{
				 var cast = await GetTvMazeCast(tvMazeShow);

				dbShows.Add(new Show
				{
					TvMazeId = tvMazeShow.Id,
					Name = tvMazeShow.Name,
					Cast = cast
				});
			}

			await _showsRepository.AddMany(dbShows.ToArray());
		}

		private async Task<Cast> GetTvMazeCast(TvMazeShowApiResponse tvMazeShow)
		{
			var castResponse =  await Policy.HandleResult<TvMazeCastResponseModel>(r => r.NeedToWait)
				.WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
				.ExecuteAsync(() => _tvMazeIntegrationService.GetCastByShow(tvMazeShow.Id));

			return new Cast
			{
				People = castResponse.Person.Select(p =>
					new Domain.Person
					{
						Birthday = p.Person.Birthday,
						Name = p.Person.Name,
						TvMazeId = p.Person.Id
					}).ToArray()
			};
		}

		private async Task<TvMazeShowApiResponse[]> GetTvMazeShows()
		{
			var showResponse = await Policy.HandleResult<TvMazeShowsResponseModel>(r => r.IsFailed)
					.RetryAsync(5)
					.ExecuteAsync(() => _tvMazeIntegrationService.GetAllShows());

			return showResponse.Shows;
		}
	}
}
