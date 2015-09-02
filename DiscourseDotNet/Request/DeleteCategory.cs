using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscourseDotNet.Request
{
	public class DeleteCategory : APIRequest
	{
		public DeleteCategory(int categoryId)
		{
			CategoryId = categoryId;
		}

		[JsonProperty("category_id")]
		public int CategoryId { get; set; }
	}
}
