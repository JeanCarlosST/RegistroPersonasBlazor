using Microsoft.EntityFrameworkCore;
using RegistroPersonasBlazor.DAL;
using RegistroPersonasBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistroPersonasBlazor.BLL
{
    public class MorasBLL
    {
        public static bool Guardar(Moras mora)
        {
            if (!Existe(mora.MoraID))
                return Insertar(mora);
            else
                return Modificar(mora);
        }
        private static bool Insertar(Moras mora)
        {
            Contexto contexto = new Contexto();
            bool found = false;

            try
            {
                contexto.Moras.Add(mora);
                found = contexto.SaveChanges() > 0;

                List<MorasDetalle> detalles = mora.Detalle;
                foreach (MorasDetalle d in detalles)
                {
                    Prestamos prestamo = PrestamosBLL.Buscar(d.PrestamoID);
                    prestamo.Mora += d.Valor;
                    PrestamosBLL.Guardar(prestamo);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return found;
        }
        public static bool Modificar(Moras mora)
        {
            Contexto contexto = new Contexto();
            bool modificado = false;

            try
            {
                List<MorasDetalle> viejosDetalles = Buscar(mora.MoraID).Detalle;
                foreach (MorasDetalle d in viejosDetalles)
                {
                    Prestamos prestamo = PrestamosBLL.Buscar(d.PrestamoID);
                    prestamo.Mora -= d.Valor;
                    PrestamosBLL.Guardar(prestamo);
                }

                contexto.Database.ExecuteSqlRaw($"delete from MorasDetalle where MoraId = {mora.MoraID}");
                foreach (var anterior in mora.Detalle)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }

                List<MorasDetalle> nuevosDetalles = mora.Detalle;
                foreach (MorasDetalle d in nuevosDetalles)
                {
                    Prestamos prestamo = PrestamosBLL.Buscar(d.PrestamoID);
                    prestamo.Mora += d.Valor;
                    PrestamosBLL.Guardar(prestamo);
                }

                contexto.Entry(mora).State = EntityState.Modified;
                modificado = contexto.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return modificado;
        }
        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool eliminado = false;

            try
            {
                var mora = contexto.Moras.Find(id);

                if (mora != null)
                {
                    List<MorasDetalle> viejosDetalles = Buscar(mora.MoraID).Detalle;
                    foreach (MorasDetalle d in viejosDetalles)
                    {
                        Prestamos prestamo = PrestamosBLL.Buscar(d.PrestamoID);
                        prestamo.Mora -= d.Valor;
                        PrestamosBLL.Guardar(prestamo);
                    }

                    contexto.Entry(mora).State = EntityState.Deleted;
                    eliminado = contexto.SaveChanges() > 0;
                }

            }
            catch
            {
                throw;

            }
            finally
            {
                contexto.Dispose();
            }

            return eliminado;
        }
        public static Moras Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Moras mora;

            try
            {
                mora = contexto.Moras
                    .Include(m => m.Detalle)
                    .Where(m => m.MoraID == id)
                    .SingleOrDefault();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return mora;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool existe = false;

            try
            {
                existe = contexto.Moras.Any(p => p.MoraID == id);
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return existe;
        }

        public static List<Moras> GetList(Expression<Func<Moras, bool>> criterio)
        {
            List<Moras> list = new List<Moras>();
            Contexto contexto = new Contexto();

            try
            {
                list = contexto.Moras.Where(criterio).AsNoTracking().ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return list;
        }
    }
}
