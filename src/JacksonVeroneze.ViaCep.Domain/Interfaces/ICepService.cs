using System.Collections.Generic;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.Domain.Dto;

namespace JacksonVeroneze.ViaCep.Domain.Interfaces
{
    public interface ICepService
    {
        Task<SearchDataResult> SearchZipCodeAsync(string value);

        Task<IList<SearchDataResult>> SearchStateAsync(string value);
    }
}
