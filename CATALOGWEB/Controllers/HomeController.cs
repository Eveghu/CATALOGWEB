using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CATALOGWEB.Models;
using CATALOGWEB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;


namespace CATALOGWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly CatwebContext _DBcontext;

        public HomeController(CatwebContext context)
        {
            _DBcontext = context;
        }

        public IActionResult Index()
        {
            List<Usuario> lista = _DBcontext.Usuarios.Include(c=> c.oRol).ToList();
            return View(lista);
        }
        public IActionResult Usuarios()
        {
            List<Usuario> lista = _DBcontext.Usuarios.Include(c => c.oRol).ToList();
            return View(lista);
        }
        public IActionResult Roles()
        {
            List<Rol> lista = _DBcontext.Rols.ToList();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Usuario_Detalle(int IdUsuario)
        {
            UsuarioVM oUsuarioVM = new UsuarioVM()
            {

                oUsuario = new Usuario(),
                oListaRol = _DBcontext.Rols.Select(rol => new SelectListItem()
                {
                    Text = rol.Rol1,
                    Value = rol.Idr.ToString()
                }).ToList()
                };
            if (IdUsuario != 0)
            {
                oUsuarioVM.oUsuario = _DBcontext.Usuarios.Find(IdUsuario);
            }
            return View(oUsuarioVM);
        }
        [HttpGet]
        public IActionResult Rol_Detalle(int IdRol)
        {
            RolVM oRolVM = new RolVM()
            {

                oRol = new Rol(),
                
            };
            if (IdRol != 0)
            {
                oRolVM.oRol = _DBcontext.Rols.Find(IdRol);
            }
            return View(oRolVM);
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Usuario_Detalle(UsuarioVM oUsuarioVM)
        {
            if (oUsuarioVM.oUsuario.Idu == 0)
            {
                _DBcontext.Usuarios.Add(oUsuarioVM.oUsuario);
                TempData["SuccessMessage"] = "¡USUARIO CREADO EXITOSAMENTE!";
            }
            else
            {
                _DBcontext.Usuarios.Update(oUsuarioVM.oUsuario);
                TempData["SuccessMessage"] = "¡USUARIO ACTUALIZADO EXITOSAMENTE!";
            }
            _DBcontext.SaveChanges();
            return RedirectToAction("Usuarios");
        }
        [HttpPost]
        public IActionResult Rol_Detalle(RolVM oRolVM)
        {
            if (oRolVM.oRol.Idr == 0)
            {
                _DBcontext.Rols.Add(oRolVM.oRol);

            }

            else
            {
                _DBcontext.Rols.Update(oRolVM.oRol);
            }
            _DBcontext.SaveChanges();
            return RedirectToAction("Index", "Home", "Roles");
        }
        [HttpGet]
            public IActionResult Eliminar(int IdUsuario)
            {
            Usuario oUsuario = _DBcontext.Usuarios.Include(r => r.oRol).Where(u => u.Idu == IdUsuario).FirstOrDefault();
            return View(oUsuario);
            }
        [HttpPost]
        public IActionResult Eliminar(Usuario oUsuario)
        {

            var usuario = _DBcontext.Usuarios.Find(oUsuario.Idu);
            if (usuario != null)
            {
                _DBcontext.Usuarios.Remove(usuario);
                _DBcontext.SaveChanges();
                TempData["SuccessMessage"] = "¡USUARIO ELIMINADO EXITOSAMENTE!";
            }
            else
            {
                TempData["ErrorMessage"] = "USUARIO NO ENCONTRADO.";
            }
            return RedirectToAction ("Usuarios");
        }
        [HttpGet]
        public IActionResult Eliminarol(int IdRol)
        {
            Rol oRol = _DBcontext.Rols.FirstOrDefault(r => r.Idr == IdRol);
            return View(oRol);
        }

        [HttpPost]
        public IActionResult EliminarRol(Rol oRol)
        {
            try
            {
                _DBcontext.Database.ExecuteSqlRaw("EXEC BAJAROL @IDR", new SqlParameter("@IDR", oRol.Idr));
                TempData["SuccessMessage"] = "Rol eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar el rol.";
            }
            return RedirectToAction("Roles", new { area = "Roles" });
        }

    }
}
