using CatalogoDeGames;
using CatalogoDeGames.InputModel;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Api.tests.Integrations.Controllers
{
    public class GameControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpCliente;

        public GameControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpCliente = _factory.CreateClient();
        }

        [Fact]
        public void Obtain_GetAll_Sucess()
        {
           var httpClientRequest = _httpCliente.GetAsync("api/version1/Games").GetAwaiter().GetResult();
                    
           Assert.Equal(System.Net.HttpStatusCode.OK, httpClientRequest.StatusCode);            
        }

        [Fact]
        public void InsertNewGame_AllFields_Created()
        {
            var gameInputModel = new GameInputModel
            {
                Name = "insert teste name",
                Producer = "insert test producer",
                Price = 123.2
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(gameInputModel), Encoding.UTF8,"application/json");

            var httpClientRequest = _httpCliente.GetAsync("api/version1/Games").GetAwaiter().GetResult();

            Assert.Equal(System.Net.HttpStatusCode.OK, httpClientRequest.StatusCode);
        }
    }
}
