using System.Collections.Generic;
using System.Linq;
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
        //     /// Method responsible for initializing the repository. ///
        //
        // Parameters:
        //   context:
        //     The context param.
        //
        public CepRepository(DatabaseContext context) : base(context)
        {
        }

        //
        // Summary:
        //     /// Method responsible for search by cep. ///
        //
        // Parameters:
        //   value:
        //     The value param.
        //
        public Task<Cep> FindByZipCodeAsync(string value)
            => _context.Set<Cep>().SingleOrDefaultAsync(x => x.Numero == value);

        //
        // Summary:
        //     /// Method responsible for search by uf. ///
        //
        // Parameters:
        //   value:
        //     The value param.
        //
        public Task<List<Cep>> FindByStateAsync(string value)
            => _context.Set<Cep>().Where(x => x.Uf == value).AsNoTracking().ToListAsync();
    }
}
