﻿using JacksonVeroneze.ViaCep.Data.Mappings;
using JacksonVeroneze.ViaCep.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.ViaCep.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Cep> Ceps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CepMapping());
        }
    }
}