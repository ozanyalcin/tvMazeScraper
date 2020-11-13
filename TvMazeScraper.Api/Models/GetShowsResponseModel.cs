using System;

namespace TvMazeScraper.Api.Models
{
	public class GetShowsResponseModel
	{
		public ShowResponseModel[] shows { get; set; }
	}

	public class ShowResponseModel
	{
		public long TvMazeId { get; set; }

		public string Name { get; set; }

		public PersonResponseModel[] Cast { get; set; }
	}

	public class PersonResponseModel
	{
		public long TvMazeId { get; set; }

		public string Name { get; set; }

		public DateTime? Birthday { get; set; }
	}

}
