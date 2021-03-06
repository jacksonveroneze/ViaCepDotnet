﻿// <auto-generated />
using System;
using JacksonVeroneze.ViaCep.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JacksonVeroneze.ViaCep.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210131233741_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("JacksonVeroneze.ViaCep.Domain.Entities.Cep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .IsUnicode(true)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("bairro");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("complemento");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<int>("Ddd")
                        .HasColumnType("int")
                        .HasColumnName("ddd");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Gia")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("gia");

                    b.Property<int>("Ibge")
                        .HasColumnType("int")
                        .HasColumnName("ibge");

                    b.Property<string>("Localidade")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("localidade");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(500)")
                        .HasColumnName("logradouro");

                    b.Property<string>("Numero")
                        .HasColumnType("char(9)")
                        .HasColumnName("numero");

                    b.Property<int>("Siafi")
                        .HasColumnType("int")
                        .HasColumnName("siafi");

                    b.Property<string>("Uf")
                        .HasColumnType("char(2)")
                        .HasColumnName("uf");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.ToTable("cep");
                });
#pragma warning restore 612, 618
        }
    }
}
