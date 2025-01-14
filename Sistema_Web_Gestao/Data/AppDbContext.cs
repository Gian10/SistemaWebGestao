using Microsoft.EntityFrameworkCore;
using Sistema_Web_Gestao.Models;

namespace Sistema_Web_Gestao.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento Tarefa -> Usuario
            modelBuilder.Entity<Tarefa>()
                .HasOne(t => t.Usuario) 
                .WithMany(u => u.Tarefas) 
                .HasForeignKey(t => t.UsuarioId) 
                .OnDelete(DeleteBehavior.Cascade); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
