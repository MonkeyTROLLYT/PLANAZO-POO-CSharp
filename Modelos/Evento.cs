using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANAZO_POO_CSharp.Modelos
{
    internal abstract class Evento
    {
        protected string _id, _nombre;

        protected string _fecha;

        protected decimal _preciobase;

        protected int _capacidad;

        protected int _cupodisponible;
        // Propiedades con encapsulamiento (getters y setters)
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public decimal PrecioBase
        {
            get { return _preciobase; }
            set
            {
                // Ejemplo de validación que vimos antes: que no sea menor a 0
                if (value < 0) _preciobase = 0;
                else _preciobase = value;
            }
        }

        public int CapacidadMaxima
        {
            get { return _capacidad; }
            set
            {
                // Validación para que la capacidad no sea menor a 1
                if (value < 1) _capacidad = 1;
                else _capacidad = value;
            }
        }

        public int CuposDisponibles
        {
            get { return _cupodisponible; }
            set { _cupodisponible = value; }
        }

        // Constructor para inicializar los datos comunes
        protected Evento(string id, string nombre, string fecha, decimal precioBase, int capacidadMaxima)
        {
            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            PrecioBase = precioBase;
            CapacidadMaxima = capacidadMaxima;
            CuposDisponibles = capacidadMaxima; // Al iniciar, los cupos disponibles son iguales a la capacidad máxima
        }

        // Método abstracto que las clases hijas obligatoriamente deben implementar
        public abstract bool CalcularPrecioFinal(int cedula ,int cantidad);
    }
}
