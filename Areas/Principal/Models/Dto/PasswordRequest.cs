namespace LudoLab_ConnectSys_Frontend.Areas.Principal.Models;

using System.ComponentModel.DataAnnotations;

public class PasswordRequest
{
    [Required(ErrorMessage = "El campo de nueva contraseña es obligatorio.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage =
            "La contraseña debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "El campo de confirmar contraseña es obligatorio.")]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; }
}