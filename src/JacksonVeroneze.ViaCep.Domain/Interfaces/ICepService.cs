using System.Collections.Generic;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.Domain.Command;

namespace JacksonVeroneze.ViaCep.Domain.Interfaces
{
    public interface ICepService
    {
        Task<SearchDataResult> SearchcAsync(string number);

        public IList<string> GetErrors();
    }
}