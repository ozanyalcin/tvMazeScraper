using Newtonsoft.Json;
using TvMazeScraper.Api.Dto;
using TvMazeScraper.Api.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TvMazeScraper.Api.Services
{
	public class TvMazeApiIntegrationService : ITvMazeApiIntegrationService
	{
		private readonly IHttpClientFactory _clientFactory;

		public TvMazeApiIntegrationService(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<TvMazeCastResponseModel> GetCastByShow(long showId)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"http://api.tvmaze.com/shows/{showId}/cast");
			request.Headers.Add("Accept", "application/json");

			var client = _clientFactory.CreateClient();

			var response = await client.SendAsync(request);

			if (response.StatusCode == HttpStatusCode.TooManyRequests)
			{
				return new TvMazeCastResponseModel
				{
					NeedToWait = true
				};
			}

			var responseStream = await response.Content.ReadAsStringAsync();
			var castResponse = JsonConvert.DeserializeObject<TvMazePersonApiResponseDto[]>(responseStream);

			return new TvMazeCastResponseModel
			{
				Person = castResponse
			};
		}

		public async Task<TvMazeShowsResponseModel> GetAllShows()
		{
			var request = new HttpRequestMessage(HttpMethod.Get, "http://api.tvmaze.com/shows");
			request.Headers.Add("Accept", "application/json");

			var client = _clientFactory.CreateClient();

			var response = await client.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				return new TvMazeShowsResponseModel
				{
					IsFailed = true
				};
			}

			var responseStream = await response.Content.ReadAsStringAsync();
			var tvMazeShows = JsonConvert.DeserializeObject<TvMazeShowApiResponse[]>(responseStream);

			return new TvMazeShowsResponseModel
			{
				Shows = tvMazeShows
			};
		}

	}
}
