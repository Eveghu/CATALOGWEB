using System.ComponentModel.DataAnnotations;

namespace CATALOGWEB.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }
    }
}
