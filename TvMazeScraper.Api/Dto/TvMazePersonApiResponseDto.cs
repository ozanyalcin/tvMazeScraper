using Newtonsoft.Json;
using System;

namespace TvMazeScraper.Api.Dto
{
	public class TvMazePersonApiResponseDto
	{
		public Person Person { get; set; }
	}

	public class Person
	{
		public long Id { get; set; }

		public Uri Url { get; set; }

		public string Name { get; set; }

		public DateTime? Birthday { get; set; }
	}
}
