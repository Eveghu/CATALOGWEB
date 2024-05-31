using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CATALOGWEB.Models;
using CATALOGWEB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace CATALOGWEB.Controllers
{
   /* [Authorize]*/
    public class HomeController : Controller
    {
        private readonly CatwebContext _DBcontext;

        public HomeController(CatwebContext context)
        {
            _DBcontext = context;
        }


        [Authorize(Roles = "Invitado")]
        public IActionResult Usuarios()
        {
            var lista = _DBcontext.Usuarios
                                 .FromSqlRaw("EXEC ConsultarUsuariosActivos")
                                 .AsEnumerable() // Ejecutar la consulta en la base de datos y convertir los resultados en una lista en el lado del cliente
                         .ToList();

            return View(lista);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
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

            oUsuarioVM.Usuarios = _DBcontext.Usuarios
                                 .FromSqlRaw("EXEC ConsultarUsuariosActivos")
                                 .AsEnumerable() // Ejecutar la consulta en la base de datos y convertir los resultados en una lista en el lado del cliente
                         .ToList();

            //oUsuarioVM.Usuarios = _DBcontext.Usuarios.Include(u => u.oRol).ToList();

            if (IdUsuario != 0)
            {
                oUsuarioVM.oUsuario = _DBcontext.Usuarios.Find(IdUsuario);
            }

            return View(oUsuarioVM);
        }



        [HttpPost]
        [Authorize(Roles = "Administrador")]
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


        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult Eliminar(int IdUsuario)
        {
            Usuario oUsuario = _DBcontext.Usuarios.FirstOrDefault(u => u.Idu == IdUsuario);
            return View(oUsuario);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Eliminar(Usuario oUsuario)
        {
            try
            {
                _DBcontext.Database.ExecuteSqlRaw("EXEC ELI @IDU", new SqlParameter("@IDU", oUsuario.Idu));
                TempData["SuccessMessage"] = "Usuario eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar el usuario: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    

}
   
}
