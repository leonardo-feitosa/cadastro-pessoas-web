using System.ComponentModel.DataAnnotations;

namespace CadastroPessoasWeb.Models
{
    public sealed class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome.")]
        [StringLength(120, ErrorMessage = "Máximo de 120 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 130, ErrorMessage = "Idade inválida.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Informe o sexo.")]
        [StringLength(20)]
        public string Sex { get; set; } = string.Empty;
    }
}