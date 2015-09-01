using Newtonsoft.Json;

namespace DiscourseDotNet.Request
{
	internal class UpdateUsername : APIRequest
	{
		internal UpdateUsername()
		{
		}

		internal UpdateUsername(string username)
		{
			Username = username;
		}

		[JsonProperty("new_username")]
		public string Username { get; set; }
	}
}