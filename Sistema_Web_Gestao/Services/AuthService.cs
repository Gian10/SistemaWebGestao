using Sistema_Web_Gestao.InterfaceRepositorio;
using Sistema_Web_Gestao.Models;

namespace Sistema_Web_Gestao.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Usuario> _repository;

        public AuthService(IRepository<Usuario> repository)
        {
            _repository = repository;
        }
        public async Task<Usuario> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _repository.GetAllAsync();
            return usuario.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

    }
}
