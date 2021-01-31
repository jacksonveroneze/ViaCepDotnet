using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JacksonVeroneze.ViaCep.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numero = table.Column<string>(type: "char(9)", nullable: true),
                    logradouro = table.Column<string>(type: "varchar(500)", nullable: true),
                    complemento = table.Column<string>(type: "varchar(500)", nullable: true),
                    bairro = table.Column<string>(type: "varchar(500)", nullable: true),
                    localidade = table.Column<string>(type: "varchar(500)", nullable: true),
                    uf = table.Column<string>(type: "char(2)", nullable: true),
                    ibge = table.Column<int>(type: "int", nullable: false),
                    gia = table.Column<string>(type: "varchar(500)", nullable: true),
                    ddd = table.Column<int>(type: "int", nullable: false),
                    siafi = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cep", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cep");
        }
    }
}
