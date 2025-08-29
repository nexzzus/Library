using System.ComponentModel.DataAnnotations;

namespace Library.Web.Entities;

public class Author
{
    [Key] public Guid Id { get; set; }
    
    [MaxLength(32, ErrorMessage = "El campo '{0} debe tener maximo {1} caracteres")]
    [Required(ErrorMessage = "El campo '{0}' es requerido")]
    [Display(Name = "Nombres")]
    public required string FirstName { get; set; }
    
    [MaxLength(32, ErrorMessage = "El campo '{0} debe tener maximo {1} caracteres")]
    [Required(ErrorMessage = "El campo '{0}' es requerido")]
    [Display(Name = "Apellidos")]
    public required string LastName { get; set; }
}