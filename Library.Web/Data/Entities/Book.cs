using System.ComponentModel.DataAnnotations;

namespace Library.Web.Entities;

public class Book
{
    [Key] 
    public Guid Id { get; set; }
    
    [MaxLength(64, ErrorMessage = "El campo '{0} debe tener maximo {1} caracteres")]
    [Required(ErrorMessage = "El campo '{0}' es requerido")]
    [Display(Name = "Nombres")]
    public required string Title { get; set; }
    
    [MaxLength(512, ErrorMessage = "El campo '{0} debe tener maximo {1} caracteres")]
    [Required(ErrorMessage = "El campo '{0}' es requerido")]
    [Display(Name = "Apellidos")]
    public required string Description { get; set; }
    
    [Required(ErrorMessage = "El campo '{0}' es requerido")]
    public required DateTime PublishDate { get; set; }
    
    public Author Author { get; set; }
    public required Guid AuthorId { get; set; }
}