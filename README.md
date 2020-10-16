## Interest

Projeto desenvolvido com o objetivo de realizar o cálculo de juros.

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
  4. Agora seus projetos estão em execução, abra o navegador e acesse: http://localhost:6001/swagger

#### Setup (Com Docker)

Siga estas etapas para para rodar o projeto em produção:

  1. Clone o repositório

  2. No diretório raiz, execute o comando:
     ```
     docker-compose build
     docker-compose up -d
     ```
  3. Agora seus projetos estão em execução, abra o navegador e acesse: http://localhost:8000/swagger

### Exemplos

#### Api - Taxa de juros

```
curl -X GET "http://localhost:5000/taxaJuros" -H "accept: application/json"

{
  "rate": 0.01
}
````

#### Api - Cálculo juros

```
curl -X GET "http://localhost:6000/calculajuros?valorInicial=100&meses=5" -H "accept: application/json"

{
  "result": 105.1
}
````

#### Api - Taxa de juros (Healthcheck)

```
curl -X GET "http://localhost:5000/health"
````

#### Api - Cálculo juros (Healthcheck)

```
curl -X GET "http://localhost:6000/health"
````

### Technologies:

- C# 8.0
- ASP.NET Core 3.1
- ASP.NET WebApi Core 3.1
- .NET Core Native DI
- Refit
- Polly
- Serilog
- Docker
- Github Actions
- SonarQube

### Autor
* **Jackson Veroneze** - *Contribuidor* - [JacksonVeroneze](http://github.com/JacksonVeroneze)


### Licença
Este projeto está licenciado sob a Licença MIT: [LICENSE.md](http://github.com/jacksonveroneze/Interest/blob/develop/LICENSE).

