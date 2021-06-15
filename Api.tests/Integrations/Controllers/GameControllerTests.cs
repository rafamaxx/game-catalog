using CatalogoDeGames;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
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
    }
}
