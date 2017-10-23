using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.Models
{
    public class Autor
    {
        [Key]
        public int AutorID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        public ICollection<LivroAutor> LivroAutor { get; set; }
    }
}
