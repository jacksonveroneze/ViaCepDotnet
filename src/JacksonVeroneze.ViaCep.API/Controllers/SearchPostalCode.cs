using System.Net.Mime;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.AntiCorruption;
using JacksonVeroneze.ViaCep.Domain;
using JacksonVeroneze.ViaCep.Domain.Entities;
using JacksonVeroneze.ViaCep.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.ViaCep.API.Controllers
{
    [ApiController]
    [Route("search-cep")]
    public class SearchPostalCode : ControllerBase
    {
        private readonly ILogger<SearchPostalCode> _logger;
        private readonly ICepHttpService _cepHttpService;
        private readonly ICepRepository _cepRepository;

        //
        // Summary:
        //     /// Method responsible for initializing the controller. ///
        //
        // Parameters:
        //   logger:
        //     The logger param.
        //
        //   cepHttpService:
        //     The cepHttpService param.
        //
        //   cepRepository:
        //     The cepRepository param.
        //
        public SearchPostalCode(ILogger<SearchPostalCode> logger, ICepHttpService cepHttpService,
            ICepRepository cepRepository)
        {
            _logger = logger;
            _cepHttpService = cepHttpService;
            _cepRepository = cepRepository;
        }

        //
        // Summary:
        //     /// Method responsible for action: Get (GET). ///
        //
        [HttpGet("{numero}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(string numero)
        {
            _logger.LogInformation("Request: {0}", "Solicitado busca de CEP");

            Cep cep = await _cepRepository.FindByCepAsync(numero);

            if (cep is null)
            {
                ViaCepResponse response = await _cepHttpService.FindAsync(numero);

                if (response != null)
                {
                    cep = new Cep(response.Cep, response.Logradouro, response.Complemento, response.Bairro,
                        response.Localidade, response.Uf, 1, 1, response.Gia);

                    await _cepRepository.AddAsync(cep);
                }

                return Ok(cep);
            }

            return Ok(cep);
        }
    }
}