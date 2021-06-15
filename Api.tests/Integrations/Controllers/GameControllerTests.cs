using CatalogoDeGames;
using CatalogoDeGames.InputModel;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.tests.Integrations.Controllers
{
    public class GameControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpCliente;
        protected GameInputModel GameInputModel;

        public GameControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpCliente = _factory.CreateClient();
        }

        [Fact]
        public async Task Obtain_GetAll_Sucess()
        {
            var httpClientRequest = await _httpCliente.GetAsync("api/version1/Games");
                    
           Assert.Equal(System.Net.HttpStatusCode.OK, httpClientRequest.StatusCode);            
        }

        [Fact]
        public async Task InsertNewGame_AllFields_Created()
        {
            var gameInputModel = new GameInputModel
            {
                Name = "insert teste name",
                Producer = "insert test producer",
                Price = 123.2
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(gameInputModel), Encoding.UTF8,"application/json");

            var httpClientRequest = await _httpCliente.GetAsync("api/version1/Games");

            Assert.Equal(System.Net.HttpStatusCode.OK, httpClientRequest.StatusCode);
        }

        public async Task InitializeAsync()
        {
            await InsertNewGame_AllFields_Created();
        }

        public async Task DisposeAsync()
        {
             _httpCliente.Dispose();
        }
    }
}
