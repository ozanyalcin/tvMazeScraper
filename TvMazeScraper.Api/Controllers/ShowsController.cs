using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TvMazeScraper.Api.Models;
using TvMazeScraper.Api.Services;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ShowsController : ControllerBase
	{
		private readonly ILogger<ShowsController> _logger;
		private readonly IShowsManagementService _showsManagementService;

		public ShowsController(
			IShowsManagementService showsManagementService,
			ILogger<ShowsController> logger)
		{
			_logger = logger;
			_showsManagementService = showsManagementService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(int pageNumber, int pageSize)
		{
			var request = new PagingRequest
			{
				PageNumber = pageNumber,
				PageSize = pageSize
			};

			var shows = await _showsManagementService.GetShows(request);

			return Ok(shows);
		}
	}
}
