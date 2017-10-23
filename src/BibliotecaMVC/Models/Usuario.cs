using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMVC.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        public virtual ICollection<Emprestimo> Emprestimo { get; set; }
    }
}
