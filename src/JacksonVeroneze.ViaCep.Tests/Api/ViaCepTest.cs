using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.API;
using JacksonVeroneze.ViaCep.Domain.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;

namespace JacksonVeroneze.ViaCep.Tests.Api
{
    public class ViaCepTest
    {
        private readonly HttpClient _httpClient;

        public ViaCepTest()
        {
            IHostBuilder hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                });

            IHost host = hostBuilder.Start();

            _httpClient = host.GetTestClient();

            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        [Fact(DisplayName = "Deve buscar corretamente os dados.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByZipCode_DeveEfetuarABuscaCalculoCorretamente()
        {
            // Arrange
            HttpResponseMessage response = await _httpClient.GetAsync("search/zip-code/89665-000");

            // Act
            string responseString = await response.Content.ReadAsStringAsync();

            SearchDataResult result = JsonConvert.DeserializeObject<SearchDataResult>(responseString);

            // Assert
            Assert.Equal("SC", result.Uf);
            Assert.Equal("Capinzal", result.Localidade);
        }

        [Fact(DisplayName = "Deve retornar erro quando informar um cep inexistênte.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByZipCode_DeveRetornarErroCepInexistente()
        {
            // Arrange && Act
            HttpResponseMessage response = await _httpClient.GetAsync("search/zip-code/89661-000");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Deve retornar erro quando informar um cep no formato inválido.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByZipCode_DeveRetornarErroCepFormatoInvalido()
        {
            // Arrange && Act
            HttpResponseMessage response = await _httpClient.GetAsync("search/zip-code/89665-0");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Deve retornar erro quando informar um estado incorreto.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByState_eveRetornarErroUfInvalido()
        {
            // Arrange && Act
            HttpResponseMessage response = await _httpClient.GetAsync("/search/state/SX");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
