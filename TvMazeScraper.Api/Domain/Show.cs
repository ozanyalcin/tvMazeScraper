using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Domain
{

	public class Show
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public long TvMazeId { get; set; }

		public string Name { get; set; }

		public Cast Cast { get; set; }
	}
}
