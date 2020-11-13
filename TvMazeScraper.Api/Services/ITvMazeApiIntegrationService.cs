using TvMazeScraper.Api.Models;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Services
{
	public interface ITvMazeApiIntegrationService
	{
		Task<TvMazeShowsResponseModel> GetAllShows();

		Task<TvMazeCastResponseModel> GetCastByShow(long showId);
	}
}