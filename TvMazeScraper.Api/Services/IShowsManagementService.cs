using TvMazeScraper.Api.Dto;
using TvMazeScraper.Api.Models;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Services
{
	public interface IShowsManagementService
	{
		Task<GetShowsResponseModel> GetShows(PagingRequest pagingRequest);
	}
}