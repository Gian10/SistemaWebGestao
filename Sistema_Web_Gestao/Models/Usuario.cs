using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Web_Gestao.Models
{
    public class Usuario
    {
        public Usuario()
        {
        }

        public Usuario(string nomeCompleto, DateTime dataNascimento, string telefoneFixo, string celular, string email, string endereco, string fotoUsuario, bool isGestor, string senha)
        {
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            TelefoneFixo = telefoneFixo;
            Celular = celular;
            Email = email;
            Endereco = endereco;
            FotoUsuario = fotoUsuario;
            IsGestor = isGestor;
            Senha = senha;
        }

        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Phone]
        public string TelefoneFixo { get; set; }

        [Phone]
        public string Celular { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Endereco { get; set; }

        public string? FotoUsuario { get; set; }  
        
        public bool IsGestor { get; set; }

        [Required]
        public string Senha { get; set; }

        [ValidateNever]
        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
