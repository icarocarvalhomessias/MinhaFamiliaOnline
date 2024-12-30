using FML.Core.Data;
using FML.File.API.Data.Entities;

namespace FML.File.API.Data.Repositories
{
    public interface IArquivoRepository : IRepository<Arquivo>
    {
        Task<bool> Adicionar(Arquivo arquivo);
        Task<bool> Atualizar(Arquivo arquivo);
        Task<bool> Remover(Arquivo arquivo);

        Task<Arquivo?> GetArquivoAsync(Guid id);
    }
}
