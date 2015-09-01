using Newtonsoft.Json;

namespace DiscourseDotNet.Request
{
	internal class UpdateCategory : APIRequest
	{
		internal UpdateCategory(int categoryId, string newName, int? parentCategoryId)
		{
			CategoryId = categoryId;
			NewName = newName;
			ParentCategoryID = parentCategoryId;
		}

		[JsonProperty("category_id")]
		public int CategoryId { get; set; }

		[JsonProperty("name")]
		public string NewName { get; set; }

		[JsonProperty("parent_category_id")]
		public int? ParentCategoryID { get; set; }
	}
}