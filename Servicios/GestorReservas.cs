using PLANAZO_POO_CSharp.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PLANAZO_POO_CSharp.Servicios
{
    internal class GestorReservas
    {
        // Listas paralelas para las reservas
        List<string> listaIds = new List<string>();
        List<string> listaCedulas = new List<string>();
        List<Evento> listaObjetos = new List<Evento>();
        List<int> listaCantidades = new List<int>();
        public void MostrarEventos(Evento[] misEventos)
        {
            Console.WriteLine("==================================================================================");
            Console.WriteLine("ID    | NOMBRE               | FECHA       | PRECIO       | CUPO / CAPACIDAD");
            Console.WriteLine("----------------------------------------------------------------------------------");

            foreach (Evento e in misEventos)
            {
                // Aquí imprimes cada fila alineada con el encabezado
                Console.WriteLine($"{e.Id,-5} | {e.Nombre,-20} | {e.Fecha,-10} | {e.PrecioBase,-12:C} | {e.CuposDisponibles}/{e.CapacidadMaxima}");
            }

            Console.WriteLine("==================================================================================");
        }
        public void consulta(string id, Evento[] misEventos)
        {
            // Usamos el método de búsqueda
            Evento e = existe(id, misEventos);

            if (e != null) 
            {
                Console.WriteLine("==================================================================================");
                Console.WriteLine("ID    | NOMBRE               | FECHA       | PRECIO       | CUPO / CAPACIDAD");
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine($"{e.Id,-5} | {e.Nombre,-20} | {e.Fecha,-10} | {e.PrecioBase,-12:C} | {e.CuposDisponibles}/{e.CapacidadMaxima}");
                Console.WriteLine("==================================================================================");
            }
            else
            {
                Console.WriteLine("No se encontro evento");
            }
        }
        public void Reservar(string id, int cedula, Evento[] misEventos, int cantidad)
        {
            Evento e = existe(id, misEventos);
            if (e != null)
            {
                if (e.CuposDisponibles < cantidad)
                {
                    Console.WriteLine("No hay suficiente cupos para reservar");
                }
                else
                {
                    // Ejecutamos el método que muestra la factura y nos devuelve true o false según la respuesta
                    bool confirmarCompra = e.CalcularPrecioFinal(cedula, cantidad);

                    if (confirmarCompra)
                    {
                        // 1. Guardamos en tus listas paralelas
                        listaIds.Add(e.Id);
                        listaCedulas.Add(cedula.ToString());
                        listaObjetos.Add(e);
                        listaCantidades.Add(cantidad);

                        // 2. Descontamos los cupos del objeto original
                        e.CuposDisponibles -= cantidad;

                        Console.WriteLine("\n¡Reserva registrada con éxito en el sistema!");
                    }
                    else
                    {
                        Console.WriteLine("\n[AVISO]: Compra cancelada por el usuario.");
                    }
                }
            }
            else Console.WriteLine("[ERROR] No existe ese evento");
        }
        public Evento existe(string id, Evento[] misEventos)
        {
            foreach (Evento e in misEventos)
            {
                if (id == e.Id)
                {
                    return e; // ¡Lo encontró y te lo entrega de inmediato!
                }
            }
            return null; // Si recorrió todo y no está, retorna nulo.
        }
        public void MostrarReservas()
        {
            // Verificamos si las listas están vacías
            if (listaIds.Count == 0)
            {
                Console.WriteLine("No hay reservas registradas en el sistema todavía.\n");
                return;
            }

            Console.WriteLine("\n==================================================================================");
            Console.WriteLine("                        LISTADO DE RESERVAS REGISTRADAS                           ");
            Console.WriteLine("==================================================================================");
            Console.WriteLine("N°R   |  CÉDULA      | ID EVENTO | NOMBRE DEL EVENTO    | CANTIDAD | OBJETO ASOCIADO");
            Console.WriteLine("----------------------------------------------------------------------------------");

          
            for (int i = 0; i < listaIds.Count; i++)
            {
                string cedula = listaCedulas[i];
                string idEvento = listaIds[i];
                Evento ev = listaObjetos[i]; 
                int cantidad = listaCantidades[i];

                // Imprimimos la fila formateada alineada
                Console.WriteLine($"{i + 1} | {cedula,-11} | {idEvento,-9} | {ev.Nombre,-20} | {cantidad,-8} | {ev.GetType().Name}");
            }

            Console.WriteLine("==================================================================================");
        }
        public void EliminarReserva(int numeroOrden)
        {
            // restamos 1 para obtener la posición real en las listas.
            int indice = numeroOrden - 1;

            // Verificamos que el número ingresado esté dentro del rango válido de las listas
            if (indice >= 0 && indice < listaIds.Count)
            {
                // 1. Recuperamos el evento y la cantidad de esa posición para devolver los cupos
                Evento ev = listaObjetos[indice];
                int cantidadReservada = listaCantidades[indice];

                // 2. Devolvemos los cupos al evento original
                ev.CuposDisponibles += cantidadReservada;

                // 3. Eliminamos de las 4 listas paralelas usando el mismo índice
                listaIds.RemoveAt(indice);
                listaCedulas.RemoveAt(indice);
                listaObjetos.RemoveAt(indice);
                listaCantidades.RemoveAt(indice);

                Console.WriteLine("\n¡Reserva eliminada con éxito y cupos devueltos!");
            }
            else
            {
                Console.WriteLine("\n[ERROR]: El número de reserva ingresado no es válido.");
            }
        }
        public void MostrarResumenes()
        {
            if (listaIds.Count == 0)
            {
                Console.WriteLine("\nNo hay reservas registradas.");
                return;
            }

            Console.WriteLine("\n==================================================================================");
            Console.WriteLine("                        RESUMEN DE RESERVAS REGISTRADAS                           ");
            Console.WriteLine("==================================================================================");
            Console.WriteLine("NRO | CÉDULA      | ID EVENTO | NOMBRE DEL EVENTO    | CANTIDAD | TIPO");
            Console.WriteLine("----------------------------------------------------------------------------------");

            for (int i = 0; i < listaIds.Count; i++)
            {
                Console.WriteLine($"{i + 1,-3} | {listaCedulas[i],-11} | {listaIds[i],-9} | {listaObjetos[i].Nombre,-20} | {listaCantidades[i],-8} | {listaObjetos[i].GetType().Name}");
            }

            Console.WriteLine("==================================================================================");
        }
    }
}
