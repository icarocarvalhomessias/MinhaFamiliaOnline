using FML.Core.Data;
using FML.File.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FML.File.API.Data.Repositories
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly ArquivosContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ArquivoRepository(ArquivosContext context)
        {
            _context = context;
        }

        public async Task<Arquivo?> GetArquivoAsync(Guid id)
        {
            return await _context.Arquivos.FindAsync(id);
        }

        public async Task<bool> Adicionar(Arquivo arquivo)
        {
            _context.Arquivos.Add(arquivo);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Atualizar(Arquivo arquivo)
        {
            _context.Arquivos.Update(arquivo);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> Remover(Arquivo arquivo)
        {
            _context.Arquivos.Remove(arquivo);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

       
    }
}
