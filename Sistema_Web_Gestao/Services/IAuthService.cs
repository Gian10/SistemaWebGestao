using Sistema_Web_Gestao.Models;

namespace Sistema_Web_Gestao.Services
{

    // Interface de Autenticação
    public interface IAuthService
    {
        Task<Usuario> AuthenticateAsync(string email, string senha);
    }
}
