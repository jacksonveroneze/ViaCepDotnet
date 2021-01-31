using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Authentication;
using AutoMapper;
using JacksonVeroneze.ViaCep.API.Configuration;
using JacksonVeroneze.ViaCep.API.Middlewares;
using JacksonVeroneze.ViaCep.Data;
using JacksonVeroneze.ViaCep.Data.Repository;
using JacksonVeroneze.ViaCep.Domain.Http;
using JacksonVeroneze.ViaCep.Domain.Interfaces;
using JacksonVeroneze.ViaCep.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Prometheus;
using Refit;

namespace JacksonVeroneze.ViaCep.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private const string AllowAllCors = "AllowAll";

        public Startup(IHostEnvironment hostEnvironment)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(5, (attempt) => TimeSpan.FromSeconds(2), (outcome, timespan, retryCount, context) =>
                    Console.WriteLine($"Tentando pela {retryCount} vez!")
                );

            services.AddRefitClient<ICepHttpService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["UrlViaCep"]))
                .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler {AllowAutoRedirect = true, ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true, SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12})
                .AddPolicyHandler(retryPolicy);

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DatabaseContext>(
                    options => options.UseSqlServer(
                        Configuration.GetConnectionString("Cep")));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<ICepService, CepService>();
            services.AddScoped<ICepRepository, CepRepository>();

            services.AddSwaggerConfiguration();

            services.AddHealthChecks();

            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllCors,
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
            });

            services.AddResponseCompression();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            CultureInfo[] supportedCultures = new[] {new CultureInfo("pt-BR")};
            app.UseRequestLocalization(new RequestLocalizationOptions {DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"), SupportedCultures = supportedCultures, SupportedUICultures = supportedCultures});

            app.UseMetricServer();

            app.UseHttpMetrics();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerConfiguration();

            app.UseHealthChecks("/health");

            app.UseCors(AllowAllCors);

            app.UseResponseCompression();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
