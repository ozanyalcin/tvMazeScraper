namespace TvMazeScraper.Api.Dto
{
	public class TvMazeCastApiResponseDto
	{
		public TvMazeCastApiResponseDto()
		{
			People = new TvMazePersonApiResponseDto[0];
		}

		public TvMazePersonApiResponseDto[] People { get; set; }
	}
}
