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

        public CepService(ICepRepository cepRepository, IMapper mapper)
        {
            _cepRepository = cepRepository;
            _mapper = mapper;
        }

        public async Task<SearchDataResult> SearchcAsync(string number)
        {
            Cep cep = await _cepRepository.FindByCepAsync(number);
            
            if (cep is null)
            {
                ViaCepResponse response = await _cepHttpService.FindAsync(number);
            
                if (response != null)
                {
                    cep = new Cep(response.Cep, response.Logradouro, response.Complemento, response.Bairro,
                        response.Localidade, response.Uf, 1, 1, response.Gia);
            
                    await _cepRepository.AddAsync(cep);
                }
            }

            return _mapper.Map<Cep, SearchDataResult>(cep);
        }
    }
}
