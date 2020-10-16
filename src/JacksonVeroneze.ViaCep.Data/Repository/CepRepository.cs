using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.BuildingBlocks;
using JacksonVeroneze.ViaCep.Domain.Entities;
using JacksonVeroneze.ViaCep.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.ViaCep.Data.Repository
{
    public class CepRepository : BaseRepository<Cep>, ICepRepository
    {
        //
        // Summary:
        //     /// Médodo responsável por inicializar o repository. ///
        //
        // Parameters:
        //   context:
        //     The context param.
        //
        public CepRepository(DatabaseContext context) : base(context)
        {
        }

        public Task<Cep> FindByCepAsync(string number)
            => _context.Set<Cep>().SingleOrDefaultAsync(x => x.Numero == number);
    }
}