using FML.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace FML.File.API.Data.Entities
{
    public class Arquivo : IAggregateRoot
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string Bucket { get; set; }
        public string Key { get; set; }
        public DateTime DataCriacao { get; set; }

        [NotMapped]
        public Stream Stream { get; set; }
    }
}
