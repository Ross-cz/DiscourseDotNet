using System;
using DiscourseDotNet.Extensions;
using DiscourseDotNet.Request;
using DiscourseDotNet.Response.Post;
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
                ParentCategoryID = 22
            };
            var response = _api.CreateCategory(category);
            Assert.IsNotNull(response);
        }

        [TestMethod, TestCategory("Online")]
        public void CreateNewTopic()
        {
            var newTopic = new NewTopic
            {
                Content =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut posuere eleifend nulla, vel interdum elit. Aenean auctor, libero in consectetur sollicitudin, dolor massa pharetra nulla, et mollis justo turpis nec tortor. Nullam lorem nunc, cursus ac pretium in, auctor eu nisi. Nullam quis lacus quam. Curabitur vel aliquet erat, sed vestibulum ante. Vivamus consequat lorem eget gravida gravida. Aenean quis arcu ut ligula facilisis aliquet. Vestibulum rutrum magna elit. Etiam sit amet malesuada quam, nec tempor mi. Nullam eu mauris dui. Sed at mi sit amet justo ultrices laoreet non eget urna. Proin felis ex, finibus et auctor a, rutrum nec elit.",
                Title = "Testing Topic: " + Guid.NewGuid(),
                CategoryID = 21
            };
            var response = _api.CreateTopic(newTopic);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
        }

        [TestMethod, TestCategory("Online")]
        public void TestSmilarPosts()
        {
            var response = _api.GetSimilarTopics("DiscourseApi", "Discourse Api post topic yes no, there");
            Assert.IsNotNull(response);
        }
    }
}