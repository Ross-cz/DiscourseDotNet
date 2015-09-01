using Newtonsoft.Json;

namespace DiscourseDotNet.Request
{
	internal class UpdateUserTrustLevel : APIRequest
	{
		internal UpdateUserTrustLevel()
		{
		}

		internal UpdateUserTrustLevel(int userId, int level)
		{
			UserId = userId;
			Level = level;
		}

		[JsonProperty("user_id")]
		public int UserId { get; set; }

		[JsonProperty("level")]
		public int Level { get; set; }
	}
}
