using System;
using DiscourseDotNet.Extensions;
using DiscourseDotNet.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiscourseDotNet.Tests
{
    [TestClass]
    public class UserTest
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("DiscourseApiKey");
        private DiscourseApi _api;

        [TestInitialize]
        public void Initialize()
        {
			_api = DiscourseApi.GetInstance(Environment.GetEnvironmentVariable("DiscourseApiUrl"), _apiKey);
        }

        [TestMethod]
        public void TestGetUser()
        {
            var result = _api.GetUser("chaoticloki");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestUpdateEmail()
        {
            var result = _api.UpdateUserEmail("cliffordduke", "cliffordduke@gmail.com");
            Assert.IsTrue(result == ResultState.Unchanged, String.Format("Actual result: {0}", result));
        }

	    [TestMethod, TestCategory("Online")]
	    public void CreateNewUser()
	    {
		    var res = _api.CreateUser("Test User", "testUser", "radim.janda@visualunity.com", "qwertyuiop");
			Assert.IsNotNull(res);
		    Assert.AreEqual(ResultState.Created, res);
	    }

	    [TestMethod, TestCategory("Online")]
	    public void UpdateUserName()
	    {
			var res = _api.UpdateUsername("newTestUser", "testUser");
			Assert.IsNotNull(res);
			Assert.AreEqual(ResultState.Modified, res);
		}

		[TestMethod, TestCategory("Online")]
		public void UpdateUserEmail()
		{
			var res = _api.UpdateUserEmail("testUser","rdm@post.cz");
			Assert.IsNotNull(res);
			Assert.AreEqual(ResultState.Modified, res);
		}

		[TestMethod, TestCategory("Online")]
		public void UpdateUserTrustLevel()
		{
			var res = _api.UpdateUserTrustLevel(18, 2);
			Assert.IsNotNull(res);
			Assert.AreEqual(ResultState.Modified, res);
		}

		[TestMethod, TestCategory("Online")]
		public void DeleteUser()
		{
			var res = _api.DeleteUser("testUser");
			Assert.IsNotNull(res);
			Assert.AreEqual(ResultState.Deleted, res);
		}
	}
}