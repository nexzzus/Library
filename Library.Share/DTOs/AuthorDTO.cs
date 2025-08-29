using System.ComponentModel.DataAnnotations;

namespace Library.Share.DTOs;

public class AuthorDTO
{
    public Guid Id { get; set; }

    [MaxLength(32, ErrorMessage = "El campo '{0} debe tener maximo {1} caracteres")]
    [Required(ErrorMessage = "El campo '{0}' es requerido")]
    [Display(Name = "Nombres")]
    public string FirstName { get; set; } = null!;

    [MaxLength(32, ErrorMessage = "El campo '{0} debe tener maximo {1} caracteres")]
    [Required(ErrorMessage = "El campo '{0}' es requerido")]
    [Display(Name = "Apellidos")]
    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";

}