using System.Net.Mime;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.ViaCep.API.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
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
        public SearchController(ILogger<SearchController> logger, ICepService cepService)
        {
            _logger = logger;
            _cepService = cepService;
        }

        //
        // Summary:
        //     /// Method responsible for action: GetByPostalCode (GET). ///
        //
        // Parameters:
        //   value:
        //     The value param.
        //
        [HttpGet("zip-code/{value}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetByZipCode(string value)
        {
            _logger.LogInformation("Request: {0}", $"Solicitado busca de CEP: [{value}].");

            return Ok(await _cepService.SearchZipCodeAsync(value));
        }

        //
        // Summary:
        //     /// Method responsible for action: GetByState (GET). ///
        //
        // Parameters:
        //   value:
        //     The value param.
        //
        [HttpGet("state/{value}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetByState(string value)
        {
            _logger.LogInformation("Request: {0}", $"Solicitado busca por UF: [{value}].");

            return Ok(await _cepService.SearchStateAsync(value));
        }
    }
}
