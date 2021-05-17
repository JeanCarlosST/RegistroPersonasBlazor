using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPersonasBlazor.DAL;
using RegistroPersonasBlazor.Models;

namespace RegistroPersonasBlazor.BLL{
    public class PersonasBLL{
        public static bool Guardar(Personas persona){
            if(!Existe(persona.PersonaID))
                return Insertar(persona); 
            else    
                return Modificar(persona);
        }
        private static bool Insertar(Personas persona){
            Contexto context = new Contexto();
            bool found = false;

            try{
                context.Personas.Add(persona);
                found = context.SaveChanges() > 0;
            
            } catch(Exception){
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static bool Modificar(Personas persona){
            Contexto context = new Contexto();
            bool found = false;

            try{
                context.Entry(persona).State = EntityState.Modified;
                found = context.SaveChanges() > 0;
            
            } catch(Exception){
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static bool Eliminar(int id){
            Contexto context = new Contexto();
            bool found = false;

            try{
                var persona = context.Personas.Find(id);

                if(persona != null){
                    context.Personas.Remove(persona);
                    found = context.SaveChanges() > 0;
                }
            
            } catch(Exception){
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }
        public static Personas Buscar(int id){
            Contexto context = new Contexto();
            Personas persona;

            try{
                persona = context.Personas.Find(id);
            
            } catch(Exception){
                throw;
            
            } finally{
                context.Dispose();
            }

            return persona;
        }
        public static bool Existe(int id){
            Contexto context = new Contexto();
            bool found = false;

            try{
                found = context.Personas.Any(p => p.PersonaID == id);
            
            } catch(Exception){
                throw;
            
            } finally{
                context.Dispose();
            }

            return found;
        }

        public static List<Personas> GetList(Expression<Func<Personas, bool>> criterio)
        {
            List<Personas> list = new List<Personas>();
            Contexto context = new Contexto();

            try
            {
                list = context.Personas.Where(criterio).AsNoTracking().ToList();
            }
            catch (Exception)
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