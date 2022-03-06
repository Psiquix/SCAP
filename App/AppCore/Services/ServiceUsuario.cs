using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Utils;

namespace AppCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {


        //        public tblUser LogIn(string mail, string pswd)
        //        {
        //            IRepUsuario rep = new RepUsuario();
        ////            string cryptPswd = Cryptography.EncrypthAES(pswd);
        //            return rep.logIn(mail, pswd);
        //        }
        public void DisableUser(int pId)
        {
            IRepUsuario oRepository = new RepUsuario();
            oRepository.DisableUser(pId);
        }

        public IEnumerable<Usuario> GetByRole(int role)
        {
            IRepUsuario oRepository = new RepUsuario();
            return oRepository.GetByRole(role);
        }

        //public IEnumerable<Usuario> GetRol()
        //{
        //    IRepUsuario oRepository = new RepUsuario();
        //    return oRepository.GetRol();
        //}

        public IEnumerable<Usuario> GetUsuario()
        {
            IRepUsuario oRepository = new RepUsuario();
            return oRepository.GetUsuario();
        }

        public Usuario GetUsuarioByID(long id)
        {
            IRepUsuario oRepository = new RepUsuario();
            return oRepository.GetUsuarioByID(id);
        }

        public Usuario LogIn(string mail, string pswd)
        {
            IRepUsuario rep = new RepUsuario();
            string cryptPswd = Cryptography.EncrypthAES(pswd);
            return rep.LogIn(mail, pswd);
        }

        public Usuario Save(Usuario user)
        {
            IRepUsuario oRepository = new RepUsuario();
            return oRepository.Save(user);
        }
    }
}
