using RestSharp;
using Newtonsoft.Json;
using FluentAssertions;
using SimpleAPITestNet.Models.PostUser;
using FluentAssertions.Execution;

namespace SimpleAPITestNet.Tests
{
    [TestFixture]
    public class PostTest
    {
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://reqres.in");
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [Test]
        public void PostUserReturnsCorrectNameAndJob()
        {
            string nameToCreate = "morpheus";
            string jobToCreate = "leader";

            request = new RestRequest("/api/users", Method.Post);
            request.AddJsonBody(new { name = nameToCreate, job = jobToCreate });

            RestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            dynamic responseData = JsonConvert.DeserializeObject(response.Content);
            Assert.That(responseData["name"].Value, Is.EqualTo(nameToCreate));
            Assert.That(responseData["job"].Value, Is.EqualTo(jobToCreate));

            response.Content.ToString().Should().Contain($"\"name\":\"{nameToCreate}\"");
            response.Content.ToString().Should().Contain($"\"job\":\"{jobToCreate}\"");
        }

        [Test]
        public void PostUserReturnsCorrectFields()
        {
            var userToPost = new User("morpheus", "leader");

            request = new RestRequest("/api/users", Method.Post);
            request.AddJsonBody(userToPost);

            RestResponse<User> response = client.Execute<User>(request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
                response.Data.name.Should().Be(userToPost.name);
                response.Data.job.Should().Be(userToPost.job);
                response.Data.id.Should().NotBeNull();
                response.Data.createdAt.Should().NotBeNull();
            }
        }
    }
}
