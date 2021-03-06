using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using JacksonVeroneze.ViaCep.Domain.Dto;
using JacksonVeroneze.ViaCep.Domain.Entities;
using JacksonVeroneze.ViaCep.Domain.Exceptions;
using JacksonVeroneze.ViaCep.Domain.Http;
using JacksonVeroneze.ViaCep.Domain.Interfaces;
using JacksonVeroneze.ViaCep.Domain.Services;
using Moq;
using Xunit;

namespace JacksonVeroneze.ViaCep.UnitTests
{
    [Collection(nameof(CepCollection))]
    public class CepServiceTest
    {
        private readonly CepServiceFixture _cepServiceFixture;
        private ICepService _cepService;

        private Mock<ICepHttpService> _cepHttpServiceMock;
        private Mock<ICepRepository> _cepRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public CepServiceTest(CepServiceFixture cepServiceFixture)
            => _cepServiceFixture = cepServiceFixture;

        [Fact(DisplayName = "DeveBuscarOsDadosNoWebServiceQuandoNaoExistirNoBancoDedados.")]
        [Trait("CepService", "FindByZipCodeAsync")]
        public async Task CepService_FindByZipCodeAsync_DeveBuscarOsDadosNoWebServiceQuandoNaoExistirNoBancoDedados()
        {
            // Arange
            _cepHttpServiceMock = new Mock<ICepHttpService>();
            _cepRepositoryMock = new Mock<ICepRepository>();
            _mapperMock = new Mock<IMapper>();

            ViaCepResponse viaCepResponseFaker = CepServiceFaker.GenerateFakerViaCepResponse().Generate();
            SearchDataResult searchDataResultFaker = CepServiceFaker.GenerateFakerSearchDataResult().Generate();

            _cepHttpServiceMock
                .Setup(x => x.FindAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(viaCepResponseFaker));

            _mapperMock
                .Setup(x => x.Map<SearchDataResult>(It.IsAny<Cep>()))
                .Returns(searchDataResultFaker);

            _cepService = new CepService(
                _cepHttpServiceMock.Object,
                _cepRepositoryMock.Object,
                _mapperMock.Object);

            // Act
            SearchDataResult result = await _cepService.SearchZipCodeAsync("89665-000");

            // Assert
            result.Should().NotBeNull();

            _cepRepositoryMock.Verify(x => x.FindByZipCodeAsync(It.IsAny<string>()), Times.Once);
            _cepHttpServiceMock.Verify(x => x.FindAsync(It.IsAny<string>()), Times.Once);
            _cepRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Cep>()), Times.Once);
            _mapperMock.Verify(x => x.Map<SearchDataResult>(It.IsAny<Cep>()), Times.Once);
        }

        [Fact(DisplayName = "DeveBuscarOsDadosNoBancoDedados.")]
        [Trait("CepService", "FindByZipCodeAsync")]
        public async Task CepService_FindByZipCodeAsync_DeveBuscarOsDadosNoBancoDedados()
        {
            // Arange
            _cepHttpServiceMock = new Mock<ICepHttpService>();
            _cepRepositoryMock = new Mock<ICepRepository>();
            _mapperMock = new Mock<IMapper>();

            Cep cepFaker = CepServiceFaker.GenerateFakerCep().Generate();
            SearchDataResult searchDataResultFaker = CepServiceFaker.GenerateFakerSearchDataResult().Generate();

            _cepRepositoryMock
                .Setup(x => x.FindByZipCodeAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(cepFaker));

            _mapperMock
                .Setup(x => x.Map<SearchDataResult>(It.IsAny<Cep>()))
                .Returns(searchDataResultFaker);

            _cepService = new CepService(
                _cepHttpServiceMock.Object,
                _cepRepositoryMock.Object,
                _mapperMock.Object);

            // Act
            SearchDataResult result = await _cepService.SearchZipCodeAsync("89665-000");

            // Assert
            result.Should().NotBeNull();

            _cepRepositoryMock.Verify(x => x.FindByZipCodeAsync(It.IsAny<string>()), Times.Once);
            _cepHttpServiceMock.Verify(x => x.FindAsync(It.IsAny<string>()), Times.Never);
            _cepRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Cep>()), Times.Never);
            _mapperMock.Verify(x => x.Map<SearchDataResult>(It.IsAny<Cep>()), Times.Once);
        }

        [Fact(DisplayName = "DeveGerarDomainExceptionQuandoInformarCepInvalido.")]
        [Trait("CepService", "FindByZipCodeAsync")]
        public async Task CepService_FindByZipCodeAsync_DeveGerarDomainExceptionQuandoInformarCepInvalido()
        {
            // Arange
            _cepHttpServiceMock = new Mock<ICepHttpService>();
            _cepRepositoryMock = new Mock<ICepRepository>();
            _mapperMock = new Mock<IMapper>();

            _cepService = new CepService(
                _cepHttpServiceMock.Object,
                _cepRepositoryMock.Object,
                _mapperMock.Object);

            // Act
            Func<Task> result = () => _cepService.SearchZipCodeAsync("89665");

            // Assert
            await result.Should().ThrowAsync<DomainException>();
        }

        [Fact(DisplayName = "DeveGerarDomainExceptionQuandoInformarCepInexistente.")]
        [Trait("CepService", "FindByZipCodeAsync")]
        public async Task CepService_FindByZipCodeAsync_DeveGerarDomainExceptionQuandoInformarCepInexistente()
        {
            // Arange
            _cepHttpServiceMock = new Mock<ICepHttpService>();
            _cepRepositoryMock = new Mock<ICepRepository>();
            _mapperMock = new Mock<IMapper>();

            _cepHttpServiceMock
                .Setup(x => x.FindAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ViaCepResponse() {Erro = true}));

            _cepService = new CepService(
                _cepHttpServiceMock.Object,
                _cepRepositoryMock.Object,
                _mapperMock.Object);

            // Act
            Func<Task> result = () => _cepService.SearchZipCodeAsync("89665-999");

            // Assert
            await result.Should().ThrowAsync<DomainException>();

            _cepRepositoryMock.Verify(x => x.FindByZipCodeAsync(It.IsAny<string>()), Times.Once);
            _cepHttpServiceMock.Verify(x => x.FindAsync(It.IsAny<string>()), Times.Once);
            _cepRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Cep>()), Times.Never);
        }


        [Fact(DisplayName = "DeveBuscarOsDadosNoWebServiceQuandoNaoExistirNoBancoDedados.")]
        [Trait("CepService", "SearchStateAsync")]
        public async Task CepService_SearchStateAsync_DeveBuscarOsDadosCorretamente()
        {
            // Arange
            _cepHttpServiceMock = new Mock<ICepHttpService>();
            _cepRepositoryMock = new Mock<ICepRepository>();
            _mapperMock = new Mock<IMapper>();

            IList<Cep> cepFaker = CepServiceFaker.GenerateFakerCep().Generate(1);
            IList<SearchDataResult> searchDataResultFaker = CepServiceFaker.GenerateFakerSearchDataResult().Generate(1);

            _cepRepositoryMock
                .Setup(x => x.FindByStateAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new List<Cep>(cepFaker)));

            _mapperMock
                .Setup(x => x.Map<List<Cep>, List<SearchDataResult>>(It.IsAny<List<Cep>>()))
                .Returns(new List<SearchDataResult>(searchDataResultFaker));

            _cepService = new CepService(
                _cepHttpServiceMock.Object,
                _cepRepositoryMock.Object,
                _mapperMock.Object);

            // Act
            IList<SearchDataResult> result = await _cepService.SearchStateAsync(cepFaker.First().Uf);

            // Assert
            result.Should().NotBeNull();

            _cepRepositoryMock.Verify(x => x.FindByStateAsync(It.IsAny<string>()), Times.Once);
            _mapperMock.Verify(x => x.Map<List<Cep>, List<SearchDataResult>>(It.IsAny<List<Cep>>()), Times.Once);
        }


        [Fact(DisplayName = "DeveGerarDomainExceptionQuandoInformarUFInexistente.")]
        [Trait("CepService", "SearchStateAsync")]
        public async Task CepService_SearchStateAsync_DeveGerarDomainExceptionQuandoInformarUFInexistente()
        {
            // Arange
            _cepHttpServiceMock = new Mock<ICepHttpService>();
            _cepRepositoryMock = new Mock<ICepRepository>();
            _mapperMock = new Mock<IMapper>();

            _cepService = new CepService(
                _cepHttpServiceMock.Object,
                _cepRepositoryMock.Object,
                _mapperMock.Object);

            // Act
            Func<Task> result = () => _cepService.SearchStateAsync("XX");

            // Assert
            await result.Should().ThrowAsync<DomainException>();
        }
    }
}
