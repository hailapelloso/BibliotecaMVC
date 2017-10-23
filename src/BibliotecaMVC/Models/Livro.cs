using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMVC.Models
{
    public class Livro
    {
        [Key]
        public int LivroID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Título")]
        [StringLength(200, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Titulo { get; set; }

        [Range(1, 300, ErrorMessage = "O campo {0} deve estar entre {1} e {2}")]
        public int Quantidade { get; set; }

        public string Foto { get; set; }

        public ICollection<LivroAutor> LivroAutor { get; set; }

        public ICollection<LivroEmprestimo> LivroEmprestimo { get; set; }

        [NotMapped]
        public IFormFile FotoFile { get; set; }
    }
}