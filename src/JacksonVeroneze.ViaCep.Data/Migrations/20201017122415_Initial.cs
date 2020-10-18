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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: true),
                    deleted_at = table.Column<DateTime>(nullable: true),
                    version = table.Column<int>(nullable: false),
                    numero = table.Column<string>(type: "char(9)", nullable: true),
                    logradouro = table.Column<string>(type: "varchar(500)", nullable: true),
                    complemento = table.Column<string>(type: "varchar(500)", nullable: true),
                    bairro = table.Column<string>(type: "varchar(500)", nullable: true),
                    localidade = table.Column<string>(type: "varchar(500)", nullable: true),
                    uf = table.Column<string>(type: "char(2)", nullable: true),
                    ibge = table.Column<int>(nullable: false),
                    gia = table.Column<string>(type: "varchar(500)", nullable: true),
                    ddd = table.Column<int>(nullable: false),
                    siafi = table.Column<int>(nullable: false)
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
