using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.Domain.Command;
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
        private readonly ICepService _cepService;

        //
        // Summary:
        //     /// Method responsible for initializing the controller. ///
        //
        // Parameters:
        //   logger:
        //     The logger param.
        //
        //   cepService:
        //     The cepService param.
        //
        public SearchPostalCode(ILogger<SearchPostalCode> logger, ICepService cepService)
        {
            _logger = logger;
            _cepService = cepService;
        }

        //
        // Summary:
        //     /// Method responsible for action: Get (GET). ///
        //
        [HttpGet("{number}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(string number)
        {
            _logger.LogInformation("Request: {0}", $"Solicitado busca de CEP: [{number}].");

            SearchDataResult result = await _cepService.SearchcAsync(number);

            if (result is null && _cepService.GetErrors().Any())
            {
                _logger.LogInformation("Request: {0}",
                    $"Houve os seguintes erros: [{string.Join(";", _cepService.GetErrors())}].");

                return BadRequest(new {Errors = _cepService.GetErrors()});
            }

            return Ok(await _cepService.SearchcAsync(number));
        }
    }
}