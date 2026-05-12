using System.ComponentModel.DataAnnotations;

namespace AspAplication.Dtos
{
    public class UpdateTaskRequest
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string Description { get; set; } = string.Empty;
    }
}