using CsvHelper.Configuration.Attributes;

namespace GeradorArquivos.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Name("Descricao")]
        public string Descricao { get; set; }
        [Name("Preco")]
        public Decimal ? Preco {  get; set; }
        [Name("Categoria")]
        public string Categoria { get; set; }
        public ICollection<NotaFiscalProduto> NotaFiscalProdutos { get; set; }
    }
}
