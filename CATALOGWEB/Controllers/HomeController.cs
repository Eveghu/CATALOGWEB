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
        [HttpGet]
        public IActionResult Index(int IdUsuario)
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

            oUsuarioVM.Usuarios = _DBcontext.Usuarios.Include(u => u.oRol).ToList();

            if (IdUsuario != 0)
            {
                oUsuarioVM.oUsuario = _DBcontext.Usuarios.Find(IdUsuario);
            }

            return View(oUsuarioVM);
        }



        [HttpPost]
        public IActionResult Index(UsuarioVM oUsuarioVM)
        {
            oUsuarioVM.oListaRol = _DBcontext.Rols.Select(rol => new SelectListItem()
            {
                Text = rol.Rol1,
                Value = rol.Idr.ToString()
            }).ToList();

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
            return RedirectToAction("Index");
        }

        public IActionResult Roles()
        {
            List<Rol> lista = _DBcontext.Rols.ToList();
            return View(lista);
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
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Eliminarol(int IdRol)
        {
            Rol oRol = _DBcontext.Rols.FirstOrDefault(r => r.Idr == IdRol);
            return View(oRol);
        }

        [HttpPost]
        public IActionResult Eliminarol(Rol oRol)
        {

            var rol = _DBcontext.Rols.Find(oRol.Idr);
            if (rol != null)
            {
                _DBcontext.Rols.Remove(rol);
                _DBcontext.SaveChanges();
                TempData["SuccessMessage"] = "¡USUARIO ELIMINADO EXITOSAMENTE!";
            }
            else
            {
                TempData["ErrorMessage"] = "USUARIO NO ENCONTRADO.";
            }
            return RedirectToAction("Index");
        }
    }
}