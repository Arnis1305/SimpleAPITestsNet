using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using SimpleAPITestNet.Models.GetUser;
using Newtonsoft.Json;

namespace SimpleAPITestNet.Tests
{
    [TestFixture]
    public class GetTest
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
        public void GetUserListReturnsStatusCodeOk()
        {
            request = new RestRequest("/api/users", Method.Get);
            request.AddQueryParameter("page", "2");

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }

        [Test]
        public void GetUserListAndMapToCustomClassCheckUserExists()
        {
            var expectedDataUser = new Data(8, "lindsay.ferguson@reqres.in", "Lindsay", "Ferguson", "https://reqres.in/img/faces/8-image.jpg");
            request = new RestRequest("/api/users", Method.Get);
            request.AddQueryParameter("page", "2");

            RestResponse response = client.Execute(request);

            var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
            var data = JsonConvert.SerializeObject(responseData["data"]);
            var dataList = JsonConvert.DeserializeObject<List<Data>>(data);


            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                dataList.Should().NotBeNull();
                dataList.Count.Should().BeGreaterThan(0);
                dataList.Should().ContainEquivalentOf(expectedDataUser);
                dataList.Exists(d => d.id == 10 && d.email == "byron.fields@reqres.in").Should().BeTrue();
            }
        }

        [Test]
        public void GetUserListAndMapToCustomClassCheckUserExistsFullDeserialize()
        {
            var expectedDataUser = new Data(8, "lindsay.ferguson@reqres.in", "Lindsay", "Ferguson", "https://reqres.in/img/faces/8-image.jpg");
            request = new RestRequest("/api/users", Method.Get);
            request.AddQueryParameter("page", "2");

            RestResponse response = client.Execute(request);

            var usersPageResponse = JsonConvert.DeserializeObject<UsersPage>(response.Content);

            usersPageResponse.data.Should().ContainEquivalentOf(expectedDataUser);
            usersPageResponse.support.url.Should().Be("https://reqres.in/#support-heading");
            usersPageResponse.per_page.Should().Be(6);
        }

        [Test]
        public void GetSecondUserDetails()
        {
            request = new RestRequest("/api/users/2", Method.Get);

            RestResponse response = client.Execute(request);

            response.Content.ToString().Should().Contain("\"id\":2");
            response.Content.ToString().Should().Contain("\"email\":\"janet.weaver@reqres.in\"");
            response.Content.ToString().Should().Contain("\"first_name\":\"Janet\"");
            response.Content.ToString().Should().Contain("\"last_name\":\"Weaver\"");
            response.Content.ToString().Should().Contain("\"avatar\":\"https://reqres.in/img/faces/2-image.jpg\"");
        }

        [Test]
        public void GetSecondUserDetailsFull()
        {
            var data = new Data(2, "janet.weaver@reqres.in", "Janet", "Weaver", "https://reqres.in/img/faces/2-image.jpg");
            var support = new Support("https://reqres.in/#support-heading", "To keep ReqRes free, contributions towards server costs are appreciated!");
            var expectedSingleUser = new SingleUser(data, support);

            request = new RestRequest("/api/users/2", Method.Get);

            RestResponse response = client.Execute(request);

            var actualSingleUser = JsonConvert.DeserializeObject<SingleUser>(response.Content);

            actualSingleUser.Should().Be(expectedSingleUser);
        }
    }
}
