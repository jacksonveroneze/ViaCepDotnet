using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using JacksonVeroneze.ViaCep.API;
using JacksonVeroneze.ViaCep.Domain.Dto;
using JacksonVeroneze.ViaCep.Domain.Entities;
using JacksonVeroneze.ViaCep.IntegrationTests.Config;
using Xunit;

namespace JacksonVeroneze.ViaCep.IntegrationTests.Api
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class ViaCepTest
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public ViaCepTest(IntegrationTestsFixture<StartupApiTests> testsFixture)
            => _testsFixture = testsFixture;

        [Fact(DisplayName = "Deve buscar corretamente os dados.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByZipCode_DeveEfetuarABuscaCepCorretamente()
        {
            // Arrange && Act
            await _testsFixture.ClearDatabase();

            HttpResponseMessage response = await _testsFixture.Client.GetAsync("search/zip-code/89665-000");

            SearchDataResult result = await _testsFixture.DeserializeObject<SearchDataResult>(response);

            // Assert
            result.Uf.Should().Be("SC");
            result.Localidade.Should().Be("Capinzal");
        }

        [Fact(DisplayName = "Deve buscar corretamente os dados.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByZipCode_DeveEfetuarABuscaCepCorretamente1()
        {
            // Arrange
            Cep cep = new Cep("89665-000", "Rua Tucano", "Perto da escola", "Recanto dos Pásaros", "Capinzal", "SC", 123456, "GIA", 49, 1);

            await _testsFixture.MockInDatabase(cep);

            // Act
            HttpResponseMessage response = await _testsFixture.Client.GetAsync("search/zip-code/89665-000");

            SearchDataResult result = await _testsFixture.DeserializeObject<SearchDataResult>(response);

            // Assert
            result.Uf.Should().Be("SC");
            result.Localidade.Should().Be("Capinzal");
        }

        [Fact(DisplayName = "Deve retornar erro quando informar um cep inexistênte.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByZipCode_DeveRetornarErroCepInexistente()
        {
            // Arrange && Act
            HttpResponseMessage response = await _testsFixture.Client.GetAsync("search/zip-code/89661-000");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Deve retornar erro quando informar um cep no formato inválido.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByZipCode_DeveRetornarErroCepFormatoInvalido()
        {
            // Arrange && Act
            HttpResponseMessage response = await _testsFixture.Client.GetAsync("search/zip-code/89665-0");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Deve retornar erro quando informar um estado incorreto.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByState_DeveRetornarErroUfInvalido()
        {
            // Arrange && Act
            HttpResponseMessage response = await _testsFixture.Client.GetAsync("/search/state/SX");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Deve retornar erro quando informar um estado incorreto.")]
        [Trait("Categoria", "SearchController")]
        public async Task SearchController_GetByState_DeveEfetuarABuscaUfCorretamente()
        {
            // Arrange
            Cep cep = new Cep("89665-000", "Rua Tucano", "Perto da escola", "Recanto dos Pásaros", "Capinzal", "SC", 123456, "GIA", 49, 1);

            await _testsFixture.MockInDatabase(cep);

            // Act
            HttpResponseMessage response = await _testsFixture.Client.GetAsync("/search/state/SC");

            IList<SearchDataResult> result = await _testsFixture.DeserializeObject<IList<SearchDataResult>>(response);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            result.Should().NotBeEmpty();
        }
    }
}
