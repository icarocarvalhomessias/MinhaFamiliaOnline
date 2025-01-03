using FML.WebApp.MVC.Services.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FML.WebApp.MVC.Services
{
    public class FamiliaService : Service, IFamiliaService
    {
        private readonly HttpClient _httpClient;

        public FamiliaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Family>> GetFamilies()
        {
            var response = await _httpClient.GetAsync("/api/familias");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<List<Family>>(response);
        }

        public async Task<List<House>> GetHousesByFamilyId(Guid familyId)
        {
            var response = await _httpClient.GetAsync($"/api/Casas/{familyId}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<List<House>>(response);
        }

        public async Task<List<Relative>> GetRelatives()
        {
            var response = await _httpClient.GetAsync("/api/familiares");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<List<Relative>>(response);
        }

        public async Task<Relative> GetRelativeById(Guid relativeId)
        {
            var response = await _httpClient.GetAsync($"/api/familiares/{relativeId}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<Relative>(response);
        }

        public async Task AddRelative(Relative relative)
        {
            var content = ObterConteudo(relative);
            var response = await _httpClient.PostAsync("/api/familiares/adicionar", content);

            TratarErrosResponse(response);
        }

        public async Task UpdateRelative(Relative relative)
        {
            var content = ObterConteudo(relative);

            var response = await _httpClient.PatchAsync("/api/familiares", content);

            TratarErrosResponse(response);
        }

        public async Task RemoveRelative(Guid relativeId)
        {
            var response = await _httpClient.DeleteAsync($"/api/familiares/{relativeId}");

            TratarErrosResponse(response);
        }

        public async Task UpdateRelative(Relative relative, Stream? fotoFile, string? fileName)
        {
            try
            {
                var updateRelativeModel = new UpdateRelativeModel
                {
                    Relative = relative,
                    FotoFileBase64 = ConvertStreamToBase64(fotoFile)
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(updateRelativeModel), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/familiares", jsonContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error updating relative: {errorMessage}");
                }

                TratarErrosResponse(response);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the relative.", ex);
            }
        }

        private string ConvertStreamToBase64(Stream? stream)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }

    public class UpdateRelativeModel
    {
        public Relative Relative { get; set; }
        public string FotoFileBase64 { get; set; }
    }
}
