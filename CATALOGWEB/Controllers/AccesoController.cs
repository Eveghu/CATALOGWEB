using Microsoft.AspNetCore.Mvc;
using CATALOGWEB.Models;
using CATALOGWEB.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CATALOGWEB.Controllers
{
    public class AccesoController : Controller
    {
        private readonly CatwebContext _context;

        public AccesoController(CatwebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            DA_Logica _da_usuario = new DA_Logica(_context);

            var usuario = await _da_usuario.ValidarUsuarioAsync(_usuario.Correo, _usuario.Contraseña);

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("Correo", usuario.Correo)
                };
                var roles = await _da_usuario.ObtenerRolesUsuarioAsync(usuario.Idu);

                // Agregar roles como claims
                foreach (var rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol.Rol1));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                return View("Index");
            }
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index"); 
        }
    }
}

