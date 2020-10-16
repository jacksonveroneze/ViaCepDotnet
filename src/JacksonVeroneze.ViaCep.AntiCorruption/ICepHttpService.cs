using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.Domain;
using Refit;

namespace JacksonVeroneze.ViaCep.AntiCorruption
{

    [Headers(new[] { "Accept: application/json;charset=UTF-8" })]
    public interface ICepHttpService
    {
        [Get("/ws/{numero}/json/")]
        Task<ViaCepResponse> FindAsync(string numero);
    }
}
