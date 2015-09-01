using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using DiscourseDotNet.Lib;
using DiscourseDotNet.Request;
using DiscourseDotNet.Response.Get;
using DiscourseDotNet.Response.Post;
using RestSharp;

namespace DiscourseDotNet.Extensions
{
	public static partial class Api
	{
		public static GetTopicsModel GetLatestTopics(this DiscourseApi api)
		{
			return api.ExecuteRequest<GetTopicsModel>("/latest.json", Method.GET);
		}

		public static GetTopicsModel GetTopTopics(this DiscourseApi api)
		{
			return api.ExecuteRequest<GetTopicsModel>("/top.json", Method.GET);
		}

		public static GetTopicsModel GetNewTopics(this DiscourseApi api, string username = DefaultUsername)
		{
			return api.ExecuteRequest<GetTopicsModel>("/new.json", Method.GET, true, username);
		}

		public static CreatedTopic CreateTopic(this DiscourseApi api, NewTopic data, string username = DefaultUsername)
		{
			return api.ExecuteRequest<CreatedTopic>("/posts", Method.POST, true, username, null, data);
		}

		public static List<SimilarTopicModel> GetSimilarTopics(this DiscourseApi api, string title, string content)
		{
			var timestamp =
				(DateTime.Now - new DateTime(1970, 1, 1).ToLocalTime()).TotalMilliseconds.ToString(
					CultureInfo.InvariantCulture);
			var parameters = new Dictionary<string, string>
			{
				{"title", title},
				{"raw", content},
				{"_", timestamp}
			};
			return api.ExecuteRequest<List<SimilarTopicModel>>("/topics/similar_to", Method.GET, false, DefaultUsername,
				parameters);
		}

		public static ResultState UpdateTopic(this DiscourseApi api, int topicId, string newTitle, int? categoryId,
			string apiUserName = DefaultUsername)
		{
			var path = String.Format("/t/{0}", topicId);
			var data = new UpdateTopic(topicId, newTitle, categoryId);

			var result = api.ExecuteRequest<RestResponse>(path, Method.PUT, true, apiUserName, null, data);

			switch (result.StatusCode)
			{
				case (HttpStatusCode) 422:
					return ResultState.Unchanged;
				case HttpStatusCode.Accepted:
					return ResultState.Modified;
				default:
					return ResultState.Error;
			}
		}
	}
}