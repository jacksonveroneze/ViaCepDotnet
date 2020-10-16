## ViaCepDotnet

Projeto desenvolvido com o objetivo de realizar buscas de CEPS no webserice do VIA CEP.

### Iniciando
Use as intruções abaixo para rodar o projeto.

#### Requisitos
Você precisará das seguintes ferramentas se desejar codificar algo:

* [Visual Studio Code ou 2019](http://www.visualstudio.com/downloads/)
* [.NET Core SDK 3.1](http://www.microsoft.com/net/download)

Você precisará das seguintes ferramentas se desejar rodar o projeto usando docker:

* [Docker](http://www.docker.com/)
* [Docker-compose](http://docs.docker.com/compose/install/)

#### Setup
Siga estas etapas para para rodar o projeto em produção:

  1. Clone o repositório

  2. No diretório raiz, restaure os pacotes (nuget) executando:
     ```
     dotnet restore
     ```
  3. Em seguida compile o projeto executando:
     ```
     dotnet build
     ```
  3. Após acesse o diretório das APIs e execute:
     ```
     dotnet run
     ```
  4. Agora seus projetos estão em execução, abra o navegador e acesse: http://localhost:5001/swagger

#### Setup (Com Docker)

Siga estas etapas para para rodar o projeto em produção:

  1. Clone o repositório

  2. No diretório raiz, execute o comando:
     ```
     docker-compose build
     docker-compose up -d
     ```
  3. Agora seus projetos estão em execução, abra o navegador e acesse: http://localhost:5000/swagger

### Exemplos

#### Api

```
curl -X GET "http://localhost:5000/search-cep/89665-000" -H "accept: application/json"

{
  "numero": "89665-000",
  "logradouro": "",
  "complemento": "",
  "bairro": "",
  "localidade": "Capinzal",
  "uf": "SC",
  "unidade": 1,
  "ibge": 1,
  "gia": ""
}
````

#### Api (Healthcheck)

```
curl -X GET "http://localhost:5000/health"
````

### Technologies:

- C# 8.0
- ASP.NET Core 3.1
- ASP.NET WebApi Core 3.1
- .NET Core Native DI
- Entity Framework Core
- Refit
- Polly
- Serilog
- SQL Server
- Docker
- Github Actions
- SonarQube

### Autor
* **Jackson Veroneze** - *Contribuidor* - [JacksonVeroneze](http://github.com/JacksonVeroneze)


### Licença
Este projeto está licenciado sob a Licença MIT: [LICENSE.md](http://github.com/jacksonveroneze/ViaCepDotnet/blob/develop/LICENSE).

