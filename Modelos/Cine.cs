using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANAZO_POO_CSharp.Modelos
{
    internal class Cine : Evento
    {

        public Cine(string id, string nombre, string fecha, decimal precioBase, int capacidadMaxima)
            : base(id, nombre, fecha, precioBase, capacidadMaxima)
        {

        }

        public override bool CalcularPrecioFinal(int cedula, int cantidad)
        {
            decimal precioModificado = _preciobase;
            decimal total = precioModificado * cantidad;

            Console.WriteLine("==========================================================");
            Console.WriteLine("                        FACTURA - CINE          ");
            Console.WriteLine("==========================================================");
            Console.WriteLine($" Película           : {Nombre}");
            Console.WriteLine($" Cédula del Cliente : {cedula}");
            Console.WriteLine($" Entradas           : {cantidad}");
            Console.WriteLine($" Precio Modificado  : {precioModificado:C}");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($" TOTAL CINE         : {total:C}");
            Console.WriteLine("==========================================================");
            Console.WriteLine("Comprar (s/n)");
            string opc = Console.ReadLine().ToLower();
            if (opc == "s") return true;
            else return false;
        }
    }
}
