using System.Net.Http.Headers;

namespace FML.Familiares.API.Clients
{
    public class FileHttp : IFileHttp
    {
        private readonly HttpClient _httpClient;

        public FileHttp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> UploadPhotoAsync(byte[] photo, string fileName)
        {
            using var content = new MultipartFormDataContent();
            using var byteArrayContent = new ByteArrayContent(photo);
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            content.Add(byteArrayContent, "file", fileName);

            return await _httpClient.PostAsync("/api/Arquivo/", content);
        }

        public async Task<string> ImageUrlAsync(Guid id)
        {
            var url = $"/api/Arquivo/url/{id}";
            var resposta = await _httpClient.GetAsync(url);
            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao baixar arquivo");
            }
            return await resposta.Content.ReadAsStringAsync();
        }


    }
}
