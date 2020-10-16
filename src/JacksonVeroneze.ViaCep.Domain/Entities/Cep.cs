using System.Numerics;
using JacksonVeroneze.ViaCep.BuildingBlocks;

namespace JacksonVeroneze.ViaCep.Domain.Entities
{
    //
    // Summary:
    //     Class responsible for the entity.
    //
    public class Cep : BaseEntity
    {
        public int Id { get; private set; }
        
        public string Numero { get; private set; }
        
        public string Logradouro { get; private set; }
        
        public string Complemento { get; private set; }
        
        public string Bairro { get; private set; }
        
        public string Localidade { get; private set; }
        
        public string Uf { get; private set; }
        
        public BigInteger Unidade { get; private set; }
        
        public int Ibge { get; private set; }
        
        public string Gia { get; private set; }

        //
        // Summary:
        //     /// Method responsible for initializing the entity. ///
        //
        public Cep() : base() { }
        
        //
        // Summary:
        //     /// Method responsible for initializing the entity. ///
        //
        // Parameters:
        //  numero:
        //     The numero param.
        //
        //  logradouro:
        //     The logradouro param.
        //
        //  complemento:
        //     The complemento param.
        //
        //  bairro:
        //     The bairro param.
        //
        //  localidade:
        //     The localidade param.
        //
        //  uf:
        //     The uf param.
        //
        //  unidade:
        //     The unidade param.
        //
        //  ibge:
        //     The ibge param.
        //
        //  gia:
        //     The gia param.
        //
        public Cep(string numero, string logradouro, string complemento, string bairro, string localidade, string uf, BigInteger unidade, int ibge, string gia)
        {
            Numero = numero;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
            Unidade = unidade;
            Ibge = ibge;
            Gia = gia;
        }
    }
}