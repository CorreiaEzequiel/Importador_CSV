using CsvHelper.Configuration.Attributes;

namespace GeradorArquivos.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Name("Nome")]
        public string Nome { get; set; }
        [Name("Email")]
        public string Email { get; set; }
        [Name("Cpf")]
        public string Cpf { get; set; }


    }
}
