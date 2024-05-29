using Microsoft.AspNetCore.Mvc.Rendering;

namespace CATALOGWEB.Models.ViewModels
{
    public class UsuarioVM
    {
        public Usuario oUsuario { get; set; }
        public List<Usuario> Usuarios { get; set; }

        public List<SelectListItem> oListaRol {  get; set; }
    }
}
