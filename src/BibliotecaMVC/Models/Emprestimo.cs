using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaMVC.Models
{
    public class Emprestimo
    {
        [Key]
        public int EmprestimoID { get; set; }

        public int UsuarioID { get; set; }

        [Display(Name = "Usuário")]
        public virtual Usuario Usuario { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de início")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de fim")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data de devolução")]
        [DataType(DataType.Date)]
        public DateTime DataDevolucao { get; set; }

        public ICollection<LivroEmprestimo> LivroEmprestimo { get; set; }
    }
}
