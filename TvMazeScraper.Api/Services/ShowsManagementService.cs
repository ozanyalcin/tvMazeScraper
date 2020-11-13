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
	public class ShowsManagementService : IShowsManagementService
	{
		public readonly IShowsRepository _showsRepository;

		public ShowsManagementService(IShowsRepository showsRepository)
		{
			_showsRepository = showsRepository;
		}

		public async Task<GetShowsResponseModel> GetShows(PagingRequest pagingRequest)
		{
			var dbShows = await _showsRepository.GetAll(pagingRequest);

			OrderByBirthday(dbShows);

			return CreateResponse(dbShows);
		}

		private GetShowsResponseModel CreateResponse(List<Show> dbShows)
		{
			return new GetShowsResponseModel
			{
				shows = dbShows.Select(s => new ShowResponseModel
				{
					Name = s.Name,
					TvMazeId = s.TvMazeId,
					Cast = s.Cast.People.Select(p => new PersonResponseModel
					{
						TvMazeId = p.TvMazeId,
						Name = p.Name,
						Birthday = p.Birthday
					}).ToArray()

				}).ToArray()
			};
		}

		private void OrderByBirthday(List<Show> dbShows)
		{
			dbShows.ForEach(s =>
			{
				s.Cast.People = s.Cast.People.OrderByDescending(p => p.Birthday).ToArray();
			});
		}
	}
}
