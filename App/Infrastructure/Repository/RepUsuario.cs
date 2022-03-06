using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepUsuario : IRepUsuario
    {

        //public Usuario logIn(string mail, string pswd)
        //{
        //    Usuario user = null;
        //    using (MyContext ctx = new MyContext())
        //    {
        //        ctx.Configuration.LazyLoadingEnabled = false;
        //        user = ctx.Usuario.Where(u => u.Email == mail && u.Pswd == pswd && u.StatusUser == true).FirstOrDefault<Usuario>();
        //    }
        //    return user;
        //}
        public void DisableUser(int pId)
        {
            int salida;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    /* La carga diferida retrasa la carga de datos relacionados,
                     * hasta que lo solicite específicamente.*/
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Usuario oUsuario = new Usuario()
                    {
                        id = pId
                    };
                    ctx.Entry(oUsuario).State = EntityState.Deleted;
                    salida = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string msj = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw new Exception(msj);
            }
            catch (Exception ex)
            {
                string msj = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref msj);
                throw;
            }
        }

        public IEnumerable<Usuario> GetByRole(int role)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Usuario> GetRol()
        //{

        //    IEnumerable<Usuario> lista = null;
        //    using (MyContext ctx = new MyContext())
        //    {
        //        ctx.Configuration.LazyLoadingEnabled = false;
        //        lista = ctx.TipoProductoes.Include(x => x.Productoes).ToList<TipoProducto>();
        //    }
        //    return lista;
        //}

        public IEnumerable<Usuario> GetUsuario()
        {
            IEnumerable<Usuario> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                //   lista = ctx.Productos.Include(x => x.idMarca).ToList<Producto>();
                lista = ctx.Usuarios.ToList<Usuario>();
            }
            return lista;
        }

        public Usuario GetUsuarioByID(long pId)
        {
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuarios.
                              Include("Rol").
                              Where(u => u.id == pId).
                              FirstOrDefault<Usuario>();
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario LogIn(string mail, string pswd)
        {
            Usuario user = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                user = ctx.Usuarios.Where(u => u.email == mail && u.contrasena == pswd && u.estado == true).FirstOrDefault<Usuario>();
            }
            return user;
        }

        public Usuario Save(Usuario user)
        {
            int salida = 0;
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioByID(user.id);
                    if (oUsuario == null)
                    {
                        user.estado = true;
                        ctx.Usuarios.Add(user);
                    }
                    else
                    {
                        ctx.Entry(user).State = EntityState.Modified;
                    }
                    salida = ctx.SaveChanges();
                }
                if (salida >= 0)
                {
                    oUsuario = GetUsuarioByID(user.id);
                }
                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
