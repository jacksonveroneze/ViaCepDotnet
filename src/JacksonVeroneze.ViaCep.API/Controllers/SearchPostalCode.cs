using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.ViaCep.API.Controllers
{

    [ApiController]
    [Route("search-cep")]
    public class SearchPostalCode : ControllerBase
    {
        private readonly ILogger<SearchPostalCode> _logger;

        //
        // Summary:
        //     /// Method responsible for initializing the controller. ///
        //
        // Parameters:
        //   logger:
        //     The logger param.
        //
        public SearchPostalCode(ILogger<SearchPostalCode> logger)
        {
            _logger = logger;
        }

        //
        // Summary:
        //     /// Method responsible for action: Get (GET). ///
        //
        [HttpGet("{numero}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public ActionResult Get(int numero)
        {
            _logger.LogInformation("Request: {0}", "Solicitado cálculo de juros.");

            return Ok("ok");
        }
    }
}
