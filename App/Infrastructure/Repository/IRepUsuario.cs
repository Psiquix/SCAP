using System;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepUsuario
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
