using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscourseDotNet.Request
{
	internal class UpdateTopic : APIRequest
	{
		internal UpdateTopic()
		{
		}

		internal UpdateTopic(int topicId, string newTitle, int? categoryId)
		{
			TopicId = topicId;
			NewTitle = newTitle;
			CategoryId = categoryId;
		}

		[JsonProperty("topic_id")]
		public int TopicId { get; set; }

		[JsonProperty("name")]
		public string NewTitle { get; set; }

		[JsonProperty("category_id")]
		public int? CategoryId { get; set; }
	}
}
