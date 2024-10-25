using Microsoft.EntityFrameworkCore;
using GeradorArquivos.Models;
namespace GeradorArquivos.Data

{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<NotaFiscalProduto> NotaFiscalProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NotaFiscalProduto>()
           .HasKey(nfp => new { nfp.NotaFiscalId, nfp.ProdutoId });
            modelBuilder.Entity<NotaFiscalProduto>()
                .HasOne(nfp => nfp.NotaFiscal)
                .WithMany(nf => nf.NotaFiscalProdutos)
                .HasForeignKey(nfp => nfp.NotaFiscalId);

            modelBuilder.Entity<NotaFiscalProduto>()
                .HasOne(nfp => nfp.Produto)
                .WithMany(p => p.NotaFiscalProdutos)
                .HasForeignKey(nfp => nfp.ProdutoId);

        }
    }

}
