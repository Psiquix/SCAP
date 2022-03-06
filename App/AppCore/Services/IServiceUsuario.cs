using Infrastructure;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IServiceUsuario
    {
        Usuario LogIn(string mail, string pswd);
        Usuario Save(Usuario user);
        Usuario GetUsuarioByID(long id);
        IEnumerable<Usuario> GetUsuario();

        IEnumerable<Usuario> GetByRole(int role);
        //IEnumerable<Usuario> GetRol();
        void DisableUser(int pId);
    }
}
