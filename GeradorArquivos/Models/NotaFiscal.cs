namespace GeradorArquivos.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public ICollection<NotaFiscalProduto> NotaFiscalProdutos{ get; set; }
       

    }
}
