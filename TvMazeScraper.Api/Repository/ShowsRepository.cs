using MongoDB.Bson;
using MongoDB.Driver;
using TvMazeScraper.Api.Domain;
using TvMazeScraper.Api.Models;
using TvMazeScraper.Api.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Repository
{
	public class ShowsRepository : IShowsRepository
	{
		private readonly IMongoCollection<Show> _shows;

		public ShowsRepository(IDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);
			_shows = database.GetCollection<Show>("Shows");
		}
		public Task<List<Show>> GetAll(PagingRequest pagingRequest)
		{
			return _shows.Find(_ => true)
				.Skip(pagingRequest.PageSize * pagingRequest.PageNumber)
				.Limit(pagingRequest.PageSize)
				.ToListAsync();
		}

		public Task AddMany(Show[] tvShows)
		{
			return _shows.InsertManyAsync(tvShows);
		}
	}
}
