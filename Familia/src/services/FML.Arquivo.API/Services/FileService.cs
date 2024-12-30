using FML.File.API.Data.Entities;
using FML.File.API.Data.Repositories;
using FML.File.API.Services.Interfaces;

namespace FML.File.API.Services
{
    public class FileService : IFileService
    {
        private readonly IAmazonS3Service _amazonS3Service;
        private readonly IArquivoRepository _arquivoRepository;

        private const string _bucket = "familia-online";

        public FileService(IAmazonS3Service amazonS3Service, IArquivoRepository arquivoRepository)
        {
            _amazonS3Service = amazonS3Service ?? throw new ArgumentNullException(nameof(amazonS3Service));
            _arquivoRepository = arquivoRepository ?? throw new ArgumentNullException(nameof(arquivoRepository));
        }

        public async Task<Arquivo?> GetFileAsync(Guid id)
        {
            var arquivo = await _arquivoRepository.GetArquivoAsync(id);

            if (arquivo == null)
                return null;

            var url =  await _amazonS3Service.GetFileUrlAsync(_bucket, arquivo.Key);

            arquivo.Caminho = url;
            return arquivo;
        }

        public async Task<Guid> SaveFileAsync(IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var arquivo = new Arquivo
            {
                Id = Guid.NewGuid(),
                Nome = file.FileName,
                Caminho = "{bucket}/{key}",
                Bucket = _bucket,
                Key = file.FileName,
                DataCriacao = DateTime.UtcNow
            };

            await _arquivoRepository.Adicionar(arquivo);

            await _amazonS3Service.SaveFileAsync(_bucket, file.FileName, file);

            return arquivo.Id;
        }
    }
}
