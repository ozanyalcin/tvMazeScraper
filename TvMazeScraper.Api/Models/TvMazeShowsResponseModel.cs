using TvMazeScraper.Api.Dto;

namespace TvMazeScraper.Api.Models
{
	public class TvMazeShowsResponseModel
	{
		public TvMazeShowsResponseModel()
		{
			Shows = new TvMazeShowApiResponse[0];
		}

		public TvMazeShowApiResponse[] Shows { get; set; }

		public bool IsFailed { get; set; } 
	}
}
