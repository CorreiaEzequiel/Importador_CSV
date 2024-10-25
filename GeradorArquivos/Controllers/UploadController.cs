using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore;
using GeradorArquivos.Data;
using GeradorArquivos.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System.Threading.Tasks;
using System.Linq;

namespace GeradorArquivos.Controllers
{
    public class UploadController : Controller
    {
        private readonly Contexto _contexto;

        public UploadController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadArquivo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Insira um arquivo CSV!";
                return View("Index");
            }

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    var csvCabecalho = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ",",
                        HeaderValidated = null, // Ignora validação de cabeçalhos
                        MissingFieldFound = null, // Ignora campos faltantes
                        HasHeaderRecord = true // Certifica-se de que a primeira linha é cabeçalho
                    });

                    // Leitura dos cabeçalhos
                    csvCabecalho.Read(); // Move para a primeira linha
                    csvCabecalho.ReadHeader(); // Lê os cabeçalhos

                    var cabecalhos = csvCabecalho.Context.Reader.HeaderRecord;

                    // Verifica se os cabeçalhos foram lidos corretamente
                    if (cabecalhos == null || !cabecalhos.Any())
                    {
                        ViewBag.Message = "Cabeçalhos do arquivo CSV não foram encontrados.";
                        return View("Index");
                    }

                    // Verifica se é arquivo de clientes
                    if (cabecalhos.Contains("Nome") && cabecalhos.Contains("Email") && cabecalhos.Contains("Cpf"))
                    {
                        var clientes = csvCabecalho.GetRecords<Cliente>().ToList();

                        if (clientes.Any(c => string.IsNullOrEmpty(c.Nome) || string.IsNullOrEmpty(c.Email) || string.IsNullOrEmpty(c.Cpf)))
                        {
                            ViewBag.Message = "Um ou mais registros de clientes contêm valores nulos.";
                            return View("Index");
                        }

                        _contexto.Clientes.AddRange(clientes);
                    }
                    // Verifica se é arquivo de produtos
                    else if (cabecalhos.Contains("Descricao") && cabecalhos.Contains("Preco") && cabecalhos.Contains("Categoria"))
                    {
                        var produtos = csvCabecalho.GetRecords<Produto>().ToList();

                        if (produtos.Any(p => string.IsNullOrEmpty(p.Descricao) || p.Preco <= 0))
                        {
                            ViewBag.Message = "Um ou mais registros de produtos contêm valores inválidos.";
                            return View("Index");
                        }

                        _contexto.Produtos.AddRange(produtos);
                    }
                    else
                    {
                        ViewBag.Message = "Formato de arquivo CSV não reconhecido.";
                        return View("Index");
                    }

                    await _contexto.SaveChangesAsync();
                    ViewBag.Message = "Dados inseridos com sucesso!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Erro ao inserir dados: {ex.Message}";
            }

            return View("Index");
        }
    }
}
