
using CATALOGWEB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CATALOGWEB.Data
{
    public class DA_Logica
    {
        private readonly CatwebContext _context;

        public DA_Logica(CatwebContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ListaUsuarioAsync()
        {
            return await _context.Usuarios
                .Include(u => u.oRol)
                .ToListAsync();
        }

        public async Task<Usuario> ValidarUsuarioAsync(string _correo, string _contraseña)
        {
            return await _context.Usuarios
                .Include(u => u.oRol)
                .FirstOrDefaultAsync(item => item.Correo == _correo && item.Contraseña == _contraseña);
        }



        public async Task<List<Rol>> ObtenerRolesUsuarioAsync(int usuarioId)
        {
            var roles = await _context.Usuarios
                .Where(u => u.Idu == usuarioId)
                .Select(u => u.oRol)
                .ToListAsync();

            return roles;
        }
    }
}