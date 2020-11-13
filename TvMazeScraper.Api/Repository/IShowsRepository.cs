using TvMazeScraper.Api.Domain;
using TvMazeScraper.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Repository
{
	public interface IShowsRepository
	{
		Task AddMany(Show[] tvShows);

		Task<List<Show>> GetAll(PagingRequest pagingRequest);
	}
}