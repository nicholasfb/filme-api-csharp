using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MinhaFilmeAPI.Data;
using MinhaFilmeAPI.Data.Dtos;
using MinhaFilmeAPI.Models;

namespace MinhaFilmeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;
public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>
        /// <response code="400">Caso os dados do corpo do JSON estejam incorretos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilme(
            [FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(BuscaFilmePorId), new { id = filme.Id }
            , filme);
            Console.WriteLine(filme.Titulo);
            Console.WriteLine(filme.DuracaoEmMinutos);
        }

        /// <summary>
        /// Lista os filmes do banco de dados
        /// </summary>
        /// <returns>IEnumerable</returns>
        /// <response code="200">Caso retorne a lista com sucesso</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ReadFilmeDto> ListaFilmes([FromQuery] int skip = 0,
            [FromQuery] int take = 5)
        {
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes
                .Skip(skip)
                .Take(take));
        }

        /// <summary>
        /// Retorna um filme específico pelo Id
        /// </summary>
        /// <param name="id">ID do filme que deseja pesquisar</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso encontre o filme pelo ID passado por parâmetro</response>
        /// <response code="404">Caso não encontre o filme que foi passado pelo ID por parâmetro</response>

        [HttpGet("{id}")]
        public IActionResult BuscaFilmePorId(int id)
        {
            var filme = _context.Filmes
                .FirstOrDefault(filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);
        }

        /// <summary>
        /// Atualiza os dados de um filme pelo ID
        /// </summary>
        /// <param name="id">ID do filme que deseja alterar os dados</param>
        /// <param name="filmeDto">JSON com a estrutura e valores que deseja atualizar</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        /// <response code="404">Caso não encontre o filme que foi passado pelo ID por parâmetro</response>

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id,
            [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();
        }


        /// <summary>
        /// Atualiza parcialmente um filme pelo ID
        /// </summary>
        /// <param name="id">ID do filme que deseja atualizar parcilamente</param>
        /// <param name="patch">JSON com os dados que deseja atualizar parcialmente</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        /// <response code="404">Caso não encontre o filme que foi passado pelo ID por parâmetro</response>
        
        [HttpPatch("{id}")]
        public IActionResult AtualizaFilmeParcial(int id,
            JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if (!TryValidateModel(filmeParaAtualizar))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta filme pelo ID
        /// </summary>
        /// <param name="id">ID do filme que deseja deletar</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a deleção seja feita com sucesso</response>
        /// <response code="404">Caso não encontre o filme que foi passado pelo ID por parâmetro</response>

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(
                filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
