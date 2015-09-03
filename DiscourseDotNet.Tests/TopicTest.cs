using System;
using DiscourseDotNet.Extensions;
using DiscourseDotNet.Lib;
using DiscourseDotNet.Request;
using DiscourseDotNet.Response.Post;
using DiscourseDotNet.Response.Post.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiscourseDotNet.Tests
{
    [TestClass]
    public class TopicTests
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("DiscourseApiKey");
        private DiscourseApi _api;

        [TestInitialize]
        public void Initialize()
        {
            _api = DiscourseApi.GetInstance(Environment.GetEnvironmentVariable("DiscourseApiUrl"), _apiKey);
        }

        [TestMethod, TestCategory("Online")]
        public void GetLatestTopicTest()
        {
            var response = _api.GetLatestTopics();

            Assert.IsNotNull(response);
        }

        [TestMethod, TestCategory("Online")]
        public void GetCategories()
        {
            var response = _api.GetCategories();

            Assert.IsNotNull(response);
        }

        [TestMethod, TestCategory("Online")]
        public void GetCategoryTopics()
        {
            var response = _api.GetCategoryTopics(2);

            Assert.IsNotNull(response);
        }

        [TestMethod, TestCategory("Online")]
        public void GetNewCategoryTopics()
        {
            var response = _api.GetNewCategoryTopics(5, "ChaoticLoki");

            Assert.IsNotNull(response);
        }

        [TestMethod, TestCategory("Online")]
        public void GetNewTopics()
        {
            var response = _api.GetNewTopics("ChaoticLoki");

            Assert.IsNotNull(response);
        }

        [TestMethod, TestCategory("Online")]
        public void CreateNewCategory()
        {
            var category = new NewCategory
            {
                Name = "API Test " + Guid.NewGuid(),
                Color = "FFA500",
                TextColor = "FFFFFF",
            };
            var response = _api.CreateCategory(category);
            Assert.IsNotNull(response);
        }

		[TestMethod, TestCategory("Online")]
		public void CreateNewCategoryAndSubCategory()
		{
			var response = CreateCategory(null);
			Assert.IsNotNull(response);

			var responseSub = CreateCategory(response.Id, "API Test SubCategory");
			Assert.IsNotNull(responseSub);
		}

		private Category CreateCategory(int? parentId, string name = "API Test Category")
	    {
			var category = new NewCategory
			{
				Name = name + Guid.NewGuid().ToString().Substring(0, 25),
				Color = "FFA500",
				TextColor = "FFFFFF",
				ParentCategoryID = parentId
			};
			return _api.CreateCategory(category);
	    }

	    [TestMethod, TestCategory("Online")]
        public void CreateNewTopic()
        {
            var response = CreateTopic(21);

		    Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
        }

	    private CreatedTopic CreateTopic(int? categoryId, string title = "Test topic: ")
	    {
		    var newTopic = new NewTopic
		    {
			    Content =
				    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut posuere eleifend nulla, vel interdum elit. Aenean auctor, libero in consectetur sollicitudin, dolor massa pharetra nulla, et mollis justo turpis nec tortor. Nullam lorem nunc, cursus ac pretium in, auctor eu nisi. Nullam quis lacus quam. Curabitur vel aliquet erat, sed vestibulum ante. Vivamus consequat lorem eget gravida gravida. Aenean quis arcu ut ligula facilisis aliquet. Vestibulum rutrum magna elit. Etiam sit amet malesuada quam, nec tempor mi. Nullam eu mauris dui. Sed at mi sit amet justo ultrices laoreet non eget urna. Proin felis ex, finibus et auctor a, rutrum nec elit.",
			    Title = title + Guid.NewGuid(),
			    CategoryID = categoryId
		    };
		    var response = _api.CreateTopic(newTopic);
		    return response;
	    }

	    [TestMethod, TestCategory("Online")]
        public void TestSmilarPosts()
        {
            var response = _api.GetSimilarTopics("DiscourseApi", "Discourse Api post topic yes no, there");
            Assert.IsNotNull(response);
		}

		#region delete

	    [TestMethod, TestCategory("Online")]
	    public void DeleteCategory()
	    {

			//Deleting Empty Category:
			var cc = CreateCategory(null, "Delete test");
			var res4 = _api.DeleteCategory(cc.Id);
			Assert.IsNotNull(res4);
			Assert.AreEqual(ResultState.Deleted, res4);

			//Deleting not existing category:
		    var res = _api.DeleteCategory(9846774);
			Assert.IsNotNull(res);
			Assert.AreEqual(ResultState.Error, res);

			//Deleting category with subcategories
			var cc1 = CreateCategory(null, "Delete test ROOT");
			var cc2 = CreateCategory(cc1.Id, "Delete test SUB");
			var res2 = _api.DeleteCategory(cc1.Id);
			Assert.IsNotNull(res2);
			Assert.AreEqual(ResultState.Deleted, res2);


			//Deleting Category with topics:
			var cct = CreateCategory(null, "Delete test Topic");
		    var topic = CreateTopic(cct.Id);
			var res3 = _api.DeleteCategory(23);
			Assert.IsNotNull(res3);
			Assert.AreEqual(ResultState.Deleted, res3);
			
		

		}

	    [TestMethod, TestCategory("Online")]
	    public void DeleteTopic()
	    {
			var topic = CreateTopic(null);
			var res = _api.DeleteTopic(topic.Post.Id);
			Assert.IsNotNull(res);
			Assert.AreEqual(ResultState.Deleted, res);
		}

	    #endregion

		#region update

		[TestMethod, TestCategory("Online")]
		public void UpdateCategory()
	    {
			var cc1 = CreateCategory(null, "Update test CAT");
		    var response = _api.UpdateCategory(cc1.Id, "UPD New cat", null);
			Assert.IsNotNull(response);
			Assert.AreEqual(ResultState.Modified, response);
		}

	    [TestMethod, TestCategory("Online")]
	    public void UpdateTopic()
	    {
			var topic = CreateTopic(null, "Test UPD Topic");
			var response = _api.UpdateTopic(topic.Post.Id, "New UPD Top", null);
			Assert.IsNotNull(response);
			Assert.AreEqual(ResultState.Modified, response);
		}

		#endregion
	}
}