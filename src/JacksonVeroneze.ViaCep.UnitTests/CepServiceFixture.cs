using System;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using JacksonVeroneze.ViaCep.Domain.Dto;
using JacksonVeroneze.ViaCep.Domain.Entities;
using JacksonVeroneze.ViaCep.Domain.Http;
using JacksonVeroneze.ViaCep.Domain.Interfaces;
using Moq;
using Xunit;

namespace JacksonVeroneze.ViaCep.UnitTests
{
    [CollectionDefinition(nameof(CepCollection))]
    public class CepCollection : ICollectionFixture<CepServiceFixture>
    {
    }

    public class CepServiceFixture : IDisposable
    {


        public CepServiceFixture()
        {
        }

        public void CreateInstances()
        {
        }

        public void Dispose()
        {
        }
    }
}
