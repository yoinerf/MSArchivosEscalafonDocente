using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MSArchivosEscalafonDocente.Models
{
    public class FilesModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo CreadoPor es obligatorio.")]
        public string CreadoPor { get; set; } = null!;

        [Required(ErrorMessage = "El campo FechaCreacion es obligatorio.")]
        public string FechaCreacion { get; set; } = null!;
        
        public string? ModificadoPor { get; set; } 
        
        public string? FechaModificacion { get; set; }

        [Required(ErrorMessage = "El campo PEGE es obligatorio.")]
        public string PEGE { get; set; } = null!;

        [Required(ErrorMessage = "El campo Documento es obligatorio.")]
        public string?  Documento { get; set; }

       // [Required(ErrorMessage = "El campo Documento es obligatorio.")]
       // [NotMapped] // Esto evita que Entity Framework intente mapear esta propiedad a una columna de base de datos
       // public IFormFile? ArchivoParaCargar { get; set; }

    }
}