using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DiscourseDotNet.Lib;
using DiscourseDotNet.Request;
using DiscourseDotNet.Response.Get;
using RestSharp;

namespace DiscourseDotNet.Extensions
{
	public static partial class Api
	{
		public static ResultState CreateUser(this DiscourseApi api, string name, string username, string email,
			string password, bool active = true, string apiUserName = DefaultUsername)
		{
			var path = "/users";
			var data = new NewUser {Active = active, Email = email, Name = name, Password = password, UserName = username};

			var result = api.ExecuteRequest<RestResponse>(path, Method.POST, true, apiUserName, null, data);
			switch (result.StatusCode)
			{
				case (HttpStatusCode) 422:
					return ResultState.Unchanged;
				case HttpStatusCode.Accepted:
					return ResultState.Created;
				default:
					return ResultState.Error;
			}
		}

		public static GetUserModel GetUser(this DiscourseApi api, string username)
		{
			var path = String.Format("/users/{0}.json", username);
			return api.ExecuteRequest<GetUserModel>(path, Method.GET);
		}

		public static ResultState UpdateUserEmail(this DiscourseApi api, string username, string newEmail,
			string apiUserName = DefaultUsername)
		{
			var path = String.Format("/users/{0}/preferences/email", username);
			var data = new UpdateEmail(newEmail);

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

		public static ResultState UpdateUsername(this DiscourseApi api, string username, string newUsername,
			string apiUserName = DefaultUsername)
		{
			var path = String.Format("/users/{0}/preferences/username", username);
			var data = new UpdateUsername(newUsername);

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

		public static ResultState UpdateUserTrustLevel(this DiscourseApi api, int userId, int level,
			string apiUserName = DefaultUsername)
		{
			var path = String.Format("/admin/users/{0}/trust_level", userId);
			var data = new UpdateUserTrustLevel(userId, level);

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

		public static ResultState DeleteUser(this DiscourseApi api, string username, string apiUserName = DefaultUsername)
		{
			var path = String.Format("/users/{0}.json", username);
			var data = new UpdateUsername(username);

			var result = api.ExecuteRequest<RestResponse>(path, Method.DELETE, true, apiUserName, null, data);

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