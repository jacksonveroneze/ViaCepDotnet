using System.Collections.Generic;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.BuildingBlocks;
using JacksonVeroneze.ViaCep.Domain.Entities;

namespace JacksonVeroneze.ViaCep.Domain.Interfaces
{
    public interface ICepRepository : IBaseRepository<Cep>
    {
        Task<Cep> FindByZipCodeAsync(string value);

        Task<List<Cep>> FindByStateAsync(string value);
    }
}
