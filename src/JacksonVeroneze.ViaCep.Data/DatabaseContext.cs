using JacksonVeroneze.ViaCep.Data.Mappings;
using JacksonVeroneze.ViaCep.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.ViaCep.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Cep> Ceps { get; set; }

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CepMapping());
        }
    }
}
