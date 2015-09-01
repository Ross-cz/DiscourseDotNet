using System;
using System.Net;
using DiscourseDotNet.Lib;
using DiscourseDotNet.Request;
using DiscourseDotNet.Response;
using DiscourseDotNet.Response.Get;
using DiscourseDotNet.Response.Post;
using DiscourseDotNet.Response.Post.Models;
using RestSharp;

namespace DiscourseDotNet.Extensions
{
    public static partial class Api
    {
        public static GetCategoriesModel GetCategories(this DiscourseApi api)
        {
            return api.ExecuteRequest<GetCategoriesModel>("/categories.json", Method.GET);
        }

        public static Category CreateCategory(this DiscourseApi api, NewCategory newCategory, string username = DefaultUsername)
        {
            var response = api.ExecuteRequest<CreatedCategory>("/categories", Method.POST, true, username, null, newCategory);
            return response == null ? null : response.Category;
        }

        public static GetTopicsModel GetCategoryTopics(this DiscourseApi api, int categoryId)
        {
            var route = String.Format("/c/{0}.json", categoryId);
            return api.ExecuteRequest<GetTopicsModel>(route, Method.GET, true);
        }

        public static GetTopicsModel GetLatestCategoryTopics(this DiscourseApi api, int categoryId)
        {
            var route = String.Format("/c/{0}/l/latest.json", categoryId);
            return api.ExecuteRequest<GetTopicsModel>(route, Method.GET);
        }

        public static GetTopicsModel GetNewCategoryTopics(this DiscourseApi api, int categoryId,
            string username = DefaultUsername)
        {
            var route = String.Format("/c/{0}/l/new.json", categoryId);
            return api.ExecuteRequest<GetTopicsModel>(route, Method.GET, true, username);
        }

        public static GetTopicsModel GetSubCategoryTopics(this DiscourseApi api, int parentCategory, int childCategory)
        {
            var route = String.Format("/c/{0}/{1}.json", parentCategory, childCategory);
            return api.ExecuteRequest<GetTopicsModel>(route, Method.GET);
        }

        public static ResultState UpdateCategory(this DiscourseApi api, int categoryId, string newName, int? parentCategory, string username = DefaultUsername)
        {
            var route = String.Format("/c/{0}", categoryId);
	        var data = new UpdateCategory(categoryId, newName, parentCategory);

            var result = api.ExecuteRequest<RestResponse>(route, Method.PUT, true, username, null, data);

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