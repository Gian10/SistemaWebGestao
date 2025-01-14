using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sistema_Web_Gestao.Models
{
    public class Tarefa
    {
        public Tarefa(string mensagem, DateTime dataLimite, int usuarioId, Usuario usuario, bool finalizada)
        {
            Mensagem = mensagem;
            DataLimite = dataLimite;
            UsuarioId = usuarioId;
            Usuario = usuario;
            Finalizada = finalizada;
        }

        public Tarefa(string mensagem, DateTime dataLimite, int usuarioId,  bool finalizada)
        {
            Mensagem = mensagem;
            DataLimite = dataLimite;
            UsuarioId = usuarioId;
            Finalizada = finalizada;
        }

        public Tarefa()
        {
        }

        public int Id { get; set; }

        [Required]
        public string Mensagem { get; set; }

        [Required]
        public DateTime DataLimite { get; set; }

        public int UsuarioId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Usuario Usuario { get; set; }

        public bool Finalizada { get; set; }
    }
}
