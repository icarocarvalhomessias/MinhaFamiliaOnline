using FML.Core.Data;
using FML.File.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FML.File.API.Data
{
    public class ArquivosContext : DbContext, IUnitOfWork
    {
        public ArquivosContext(DbContextOptions<ArquivosContext> options) : base(options)
        {
        }

        public DbSet<Arquivo> Arquivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureArquivoEntity(modelBuilder);
        }

        private void ConfigureArquivoEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arquivo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.Caminho)
                      .IsRequired()
                      .HasMaxLength(500);
                entity.Property(e => e.Bucket)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.Key)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.DataCriacao)
                      .IsRequired();
            });
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
