﻿using System.Threading.Tasks;
using Refit;

namespace JacksonVeroneze.ViaCep.Domain.Http
{

    [Headers(new[] { "Accept: application/json;charset=UTF-8" })]
    public interface ICepHttpService
    {
        [Get("/ws/{numero}/json/")]
        Task<ViaCepResponse> FindAsync(string numero);
    }
}