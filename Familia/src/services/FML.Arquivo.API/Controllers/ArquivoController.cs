using FML.File.API.Services.Interfaces;
using FML.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FML.File.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : MainController
    {
        private readonly IFileService _fileService;

        public ArquivoController(IFileService fileService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var arquivo = await _fileService.GetFileAsync(id);

            if (arquivo == null && arquivo.Stream == null)
            {
                return NotFound();
            }

            return File(arquivo.Stream, "application/octet-stream", arquivo.Nome);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                AdicionarErroProcessamento("Invalid file.");
                return CustomResponse();
            }

            var result = await _fileService.SaveFileAsync(file);

            return CustomResponse(result);
        }
        
        [HttpGet("url/{id}")]
        public async Task<IActionResult> GetFileUrl(Guid id)
        {
            var arquivo = await _fileService.GetFileAsync(id);
            if (arquivo == null)
            {
                return NotFound();
            }
            return CustomResponse(arquivo.Caminho);
        }
    }
}
