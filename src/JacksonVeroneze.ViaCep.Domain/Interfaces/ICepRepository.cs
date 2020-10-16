using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.BuildingBlocks;
using JacksonVeroneze.ViaCep.Domain.Entities;

namespace JacksonVeroneze.ViaCep.Domain.Interfaces
{
    public interface ICepRepository : IBaseRepository<Cep>
    {
        Task<Cep> FindByCepAsync(string number);
    }
}