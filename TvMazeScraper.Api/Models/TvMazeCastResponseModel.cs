using TvMazeScraper.Api.Dto;

namespace TvMazeScraper.Api.Models
{
	public class TvMazeCastResponseModel
	{
		public TvMazeCastResponseModel()
		{
			Person = new TvMazePersonApiResponseDto[0];
		}

		public TvMazePersonApiResponseDto[] Person { get; set; }

		public bool NeedToWait { get; set; }
	}
}
