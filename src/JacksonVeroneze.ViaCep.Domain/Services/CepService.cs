using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using JacksonVeroneze.ViaCep.Domain.Dto;
using JacksonVeroneze.ViaCep.Domain.Entities;
using JacksonVeroneze.ViaCep.Domain.Exceptions;
using JacksonVeroneze.ViaCep.Domain.Http;
using JacksonVeroneze.ViaCep.Domain.Interfaces;
using JacksonVeroneze.ViaCep.Domain.Util;

namespace JacksonVeroneze.ViaCep.Domain.Services
{
    public class CepService : ICepService
    {
        private readonly ICepHttpService _cepHttpService;
        private readonly ICepRepository _cepRepository;
        private readonly IMapper _mapper;

        //
        // Summary:
        //     /// Method responsible for initializing the service. ///
        //
        // Parameters:
        //   cepHttpService:
        //     The cepHttpService param.
        //
        //   cepRepository:
        //     The cepRepository param.
        //
        //   mapper:
        //     The mapper param.
        //
        public CepService(ICepHttpService cepHttpService, ICepRepository cepRepository, IMapper mapper)
        {
            _cepHttpService = cepHttpService;
            _cepRepository = cepRepository;
            _mapper = mapper;
        }

        //
        // Summary:
        //     /// Method responsible for search by cep. ///
        //
        // Parameters:
        //   value:
        //     The value param.
        //
        public async Task<SearchDataResult> SearchZipCodeAsync(string value)
        {
            if (value.Length != 9 || Regex.IsMatch(value, "\\d{5}-\\d{3}") is false)
                throw new DomainException("O CEP informado é inválido.");

            Cep postalCode = await _cepRepository.FindByZipCodeAsync(value);

            if (postalCode != null)
                return _mapper.Map<Cep, SearchDataResult>(postalCode);

            ViaCepResponse response = await _cepHttpService.FindAsync(value);

            if (response.Erro is true)
                throw new DomainException("O CEP informado não foi encontrado no webservice VIACEP.");

            postalCode = new Cep(response.Cep, response.Logradouro, response.Complemento, response.Bairro,
                response.Localidade, response.Uf, response.Ibge, response.Gia, response.Ddd, response.Siafi);

            await _cepRepository.AddAsync(postalCode);

            return _mapper.Map<Cep, SearchDataResult>(postalCode);
        }

        //
        // Summary:
        //     /// Method responsible for search by uf. ///
        //
        // Parameters:
        //   value:
        //     The value param.
        //
        public async Task<IList<SearchDataResult>> SearchStateAsync(string value)
        {
            if (value.Length != 2 || ListStates.List.Contains(value.ToUpper()) is false)
                throw new DomainException("O estado informado não é válido.");

            List<Cep> listPostalCode = await _cepRepository.FindByStateAsync(value);

            return _mapper.Map<List<Cep>, List<SearchDataResult>>(listPostalCode);
        }
    }
}
