using JacksonVeroneze.ViaCep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JacksonVeroneze.ViaCep.Data.Mappings
{
    public class CepMapping : IEntityTypeConfiguration<Cep>
    {
        public void Configure(EntityTypeBuilder<Cep> builder)
        {
            builder.ToTable("cep");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Numero)
                .HasColumnName("numero")
                .HasColumnType("char(9)");

            builder.Property(c => c.Logradouro)
                .HasColumnName("logradouro")
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Complemento)
                .HasColumnName("complemento")
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Bairro)
                .HasColumnName("bairro")
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Localidade)
                .HasColumnName("localidade")
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Uf)
                .HasColumnName("uf")
                .HasColumnType("char(2)");

            builder.Property(c => c.Unidade)
                .HasColumnName("unidade");

            builder.Property(c => c.Ibge)
                .HasColumnName("ibge");

            builder.Property(c => c.Gia)
                .HasColumnName("gia")
                .HasColumnType("varchar(500)");
        }
    }
}