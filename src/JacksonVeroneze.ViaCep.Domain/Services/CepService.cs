using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JacksonVeroneze.ViaCep.Domain.Command;
using JacksonVeroneze.ViaCep.Domain.Entities;
using JacksonVeroneze.ViaCep.Domain.Http;
using JacksonVeroneze.ViaCep.Domain.Interfaces;

namespace JacksonVeroneze.ViaCep.Domain.Services
{
    public class CepService : ICepService
    {
        private readonly ICepHttpService _cepHttpService;
        private readonly ICepRepository _cepRepository;
        private readonly IMapper _mapper;

        public readonly IList<string> _errors = new List<string>();

        public CepService(ICepHttpService cepHttpService, ICepRepository cepRepository, IMapper mapper)
        {
            _cepHttpService = cepHttpService;
            _cepRepository = cepRepository;
            _mapper = mapper;
        }

        public async Task<SearchDataResult> SearchZipCodeAsync(string value)
        {
            if (value.Length != 9)
            {
                _errors.Add("O CEP informado é inválido");

                return null;
            }

            Cep postalCode = await _cepRepository.FindByZipCodeAsync(value);

            if (postalCode is null)
            {
                ViaCepResponse response = await _cepHttpService.FindAsync(value);

                if (response != null)
                {
                    postalCode = new Cep(response.Cep, response.Logradouro, response.Complemento, response.Bairro, response.Localidade, response.Uf, response.Ibge, response.Gia, response.Ddd, response.Siafi);

                    await _cepRepository.AddAsync(postalCode);
                }
            }

            return _mapper.Map<Cep, SearchDataResult>(postalCode);
        }

        public async Task<IList<SearchDataResult>> SearchStateAsync(string value)
        {
            if (value.Length != 2)
            {
                _errors.Add("O estado informado é inválido");

                return null;
            }

            List<Cep> listPostalCode = await _cepRepository.FindByStateAsync(value);

            return _mapper.Map<List<Cep>, List<SearchDataResult>>(listPostalCode);
        }

        public IList<string> GetErrors()
        {
            return _errors;
        }
    }
}
