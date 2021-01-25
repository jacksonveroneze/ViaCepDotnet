using System;
using System.Net.Http;
using System.Threading.Tasks;
using JacksonVeroneze.ViaCep.API;
using JacksonVeroneze.ViaCep.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace JacksonVeroneze.ViaCep.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>>
    {
    }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly AppFactory<TStartup> Factory;
        public HttpClient Client;
        public readonly DatabaseContext _context;

        public IntegrationTestsFixture()
        {
            WebApplicationFactoryClientOptions clientOptions = new WebApplicationFactoryClientOptions {AllowAutoRedirect = true, HandleCookies = true, MaxAutomaticRedirections = 7};

            Factory = new AppFactory<TStartup>();
            Client = Factory.CreateClient(clientOptions);

            _context = (DatabaseContext)Factory.Services.GetService(typeof(DatabaseContext));
        }

        public async Task<T> DeserializeObject<T>(HttpResponseMessage response)
        {
            string responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public async Task MockInDatabase<T>(T entity) where T : class
        {
            await ClearDatabase();

            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task ClearDatabase()
            => await _context.Database.EnsureDeletedAsync();

        public void Dispose()
        {
            Factory?.Dispose();
            Client?.Dispose();
            _context?.Dispose();
        }
    }
}
