using System;

namespace TvMazeScraper.Api.Domain
{
	public class Person
	{
		public long TvMazeId { get; set; }

		public string Name { get; set; }

		public DateTime? Birthday { get; set; }
	}
}