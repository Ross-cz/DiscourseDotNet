using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscourseDotNet.Request
{
	public class DeleteTopic : APIRequest
	{
		public DeleteTopic(int topicId)
		{
			TopicId = topicId;
		}

		[JsonProperty("topic_id")]
		public int TopicId { get; set; }
	}
}
