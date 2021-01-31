using Bogus;
using JacksonVeroneze.ViaCep.Domain.Dto;
using JacksonVeroneze.ViaCep.Domain.Entities;

namespace JacksonVeroneze.ViaCep.UnitTests
{
    public class CepServiceFaker
    {
        public static Faker<Cep> GenerateFakerCep()
        {
            return new Faker<Cep>()
                .RuleFor(x => x.Numero, f => f.Address.ZipCode())
                .RuleFor(x => x.Logradouro, f => f.Address.StreetName())
                .RuleFor(x => x.Complemento, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Bairro, f => f.Address.Direction())
                .RuleFor(x => x.Localidade, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Uf, "SC")
                .RuleFor(x => x.Ibge, 020423)
                .RuleFor(x => x.Gia, f => f.Address.StateAbbr())
                .RuleFor(x => x.Ddd, 49)
                .RuleFor(x => x.Siafi, 0);
        }

        public static Faker<ViaCepResponse> GenerateFakerViaCepResponse()
        {
            return new Faker<ViaCepResponse>()
                .RuleFor(x => x.Cep, f => f.Address.ZipCode())
                .RuleFor(x => x.Logradouro, f => f.Address.StreetName())
                .RuleFor(x => x.Complemento, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Bairro, f => f.Address.Direction())
                .RuleFor(x => x.Localidade, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Uf, f => f.Address.StateAbbr())
                .RuleFor(x => x.Ibge, 020423)
                .RuleFor(x => x.Gia, f => f.Address.StateAbbr())
                .RuleFor(x => x.Ddd, 49)
                .RuleFor(x => x.Siafi, 0);
        }

        public static Faker<SearchDataResult> GenerateFakerSearchDataResult()
        {
            return new Faker<SearchDataResult>()
                .RuleFor(x => x.Numero, f => f.Address.ZipCode())
                .RuleFor(x => x.Logradouro, f => f.Address.StreetName())
                .RuleFor(x => x.Complemento, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Bairro, f => f.Address.Direction())
                .RuleFor(x => x.Localidade, f => f.Address.SecondaryAddress())
                .RuleFor(x => x.Uf, f => f.Address.StateAbbr())
                .RuleFor(x => x.Ibge, 020423)
                .RuleFor(x => x.Gia, f => f.Address.StateAbbr())
                .RuleFor(x => x.Ddd, 49)
                .RuleFor(x => x.Siafi, 0);
        }
    }
}
