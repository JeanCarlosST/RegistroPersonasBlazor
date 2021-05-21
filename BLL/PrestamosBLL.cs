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
    public class PrestamosBLL
    {
        public static bool Guardar(Prestamos prestamo)
        {
            if (!Existe(prestamo.PrestamoID))
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }
        private static bool Insertar(Prestamos prestamo)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                prestamo.Balance = prestamo.Monto;
                context.Prestamos.Add(prestamo);
                found = context.SaveChanges() > 0;

                Personas persona = PersonasBLL.Buscar(prestamo.PersonaID);
                persona.Balance += prestamo.Monto;
                PersonasBLL.Modificar(persona);

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }
        public static bool Modificar(Prestamos prestamo)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                prestamo.Balance = prestamo.Monto;
                Prestamos viejoPrestamo = Buscar(prestamo.PrestamoID);
                float nuevoMonto = prestamo.Monto - viejoPrestamo.Monto;

                Personas persona = PersonasBLL.Buscar(prestamo.PersonaID);
                persona.Balance += nuevoMonto;
                PersonasBLL.Modificar(persona);

                context.Entry(prestamo).State = EntityState.Modified;
                found = context.SaveChanges() > 0;

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }
        public static bool Eliminar(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                var prestamo = context.Prestamos.Find(id);

                if (prestamo != null)
                {
                    Personas persona = PersonasBLL.Buscar(prestamo.PersonaID);
                    persona.Balance -= prestamo.Monto;
                    PersonasBLL.Modificar(persona);

                    context.Prestamos.Remove(prestamo);
                    found = context.SaveChanges() > 0;
                }

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }
        public static Prestamos Buscar(int id)
        {
            Contexto context = new Contexto();
            Prestamos prestamo;

            try
            {
                prestamo = context.Prestamos
                    .Where(p => p.PrestamoID == id)
                    .SingleOrDefault();

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return prestamo;
        }
        public static bool Existe(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                found = context.Prestamos.Any(p => p.PrestamoID == id);

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> list = new List<Prestamos>();
            Contexto context = new Contexto();

            try
            {
                list = context.Prestamos.Where(criterio).AsNoTracking().ToList();

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return list;
        }
    }
}
