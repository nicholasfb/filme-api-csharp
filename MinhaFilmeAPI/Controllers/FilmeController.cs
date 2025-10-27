using Microsoft.AspNetCore.Mvc;
using MinhaFilmeAPI.Models;

namespace MinhaFilmeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]
        public void AdicionaFilme([FromBody]Filme filme)
        {
            filmes.Add(filme);
            Console.WriteLine(filme.Titulo);
            Console.WriteLine(filme.DuracaoEmMinutos);
        }
    }
}
