using System.ComponentModel.DataAnnotations;

namespace MinhaFilmeAPI.Data.Dtos;

public class CreateFilmeDto
{
    [Required(ErrorMessage = "O título do filme é obrigatório")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    [StringLength(50, ErrorMessage = "O gênero do filme não pode exceder 50 caracteres")]
    public string Genero { get; set; }
    [Required]
    [Range(70, 600, ErrorMessage = "A duração do filme deve estar entre 70 e 600 minutos")]
    public int DuracaoEmMinutos { get; set; }
}
