namespace JacksonVeroneze.ViaCep.Domain.Command
{
    public class SearchDataResult
    {
        public string Numero { get; set; }

        public string Logradouro { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Uf { get; set; }

        public int Ibge { get; set; }

        public string Gia { get; set; }

        public int Ddd { get; set; }

        public int Siafi { get; set; }
    }
}
