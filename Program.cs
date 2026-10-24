//El md dice que tenemos que tener los eventos precargados
using PLANAZO_POO_CSharp.Modelos;
using PLANAZO_POO_CSharp.Servicios;

Cine Cine1 = new Cine("E01", "Avatar", "2026-06-10", 15000m, 50);
Cine Cine2 = new Cine("E02", "Dune", "2026-06-12", 18000m, 40);

Concierto Concierto1 = new Concierto("E03", "Rock Sinfónico", "2026-06-15", 80000m, 100);
Concierto Concierto2 = new Concierto("E04", "Festival Urbano", "2026-06-20", 120000m, 150);

Taller Taller1 = new Taller("E05", "Taller C#", "2026-06-05", 50000m, 20);
Taller Taller2 = new Taller("E06", "Taller POO", "2026-06-08", 40000m, 25);

//Obejto para Gestor de reservar
GestorReservas admin = new GestorReservas();
Evento[] misEventos = { Cine1, Cine2, Concierto1, Concierto2, Taller1, Taller2 };

//Variable de datos ingresados
string opc, id;
int cedula, cantidad, opc1;
//Uso While true
//por que voy a usar una Environment.Exit(0); de cuando sea 0 finalize el programa
while (true)
{
    menu();
    opc = Console.ReadLine();
    switch (opc)
    {
        case "0":
            Console.WriteLine("Finalizando programa");
            Environment.Exit(0);
            break;
        case "1":
            admin.MostrarEventos(misEventos);
            break;
        case "2":
            Console.Write("Digite Id el evento a consultar: ");
            id= Console.ReadLine();
            admin.consulta(id, misEventos);
            break;
        case "3":
            try
            {
                Console.Write("Digite su cedula:");
                cedula = int.Parse(Console.ReadLine());
                Console.Write("Digite Id el evento a consultar: ");
                id = Console.ReadLine();
                Console.Write("Digite la cantidad:");
                cantidad = int.Parse(Console.ReadLine());
                admin.Reservar(id, cedula, misEventos, cantidad);
            }
            catch (FormatException)
            {
                Console.WriteLine("[ERROR]: Debe ingresar solo números enteros. Inténtelo de nuevo.\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine("[ERROR]: El número de cédula es demasiado grande. Inténtelo de nuevo.\n");
            }
            break;
        case "4":
            admin.MostrarReservas();
            break;
        case "5":
            admin.MostrarReservas();
            try
            {
                Console.WriteLine("Digite la reserva a cancelar: ");
                opc1 = int.Parse(Console.ReadLine());
                admin.EliminarReserva(opc1);
            }
            catch (FormatException)
            {
                Console.WriteLine("[ERROR]: Debe ingresar solo números enteros. Inténtelo de nuevo.\n");
            }
            catch (OverflowException)
            {
                Console.WriteLine("[ERROR]: El número de cédula es demasiado grande. Inténtelo de nuevo.\n");
            }
            break;
        case "6":
            admin.MostrarResumenes();

            break;
        default:
            Console.WriteLine("Opcion Invalida por favor digite un de las opciones diponibles (0-6)");
            break;
    }
}
void menu()
{
    Console.WriteLine("================================");
    Console.WriteLine("        PLANAZO - EVENTOS       ");
    Console.WriteLine("================================");
    Console.WriteLine("1. Ver eventos disponibles");
    Console.WriteLine("2. Consultar evento");
    Console.WriteLine("3. Reservar entradas");
    Console.WriteLine("4. Ver reservas realizadas");
    Console.WriteLine("5. Cancelar una reserva");
    Console.WriteLine("6. Ver resumen de reservas");
    Console.WriteLine("0. Salir");
    Console.Write("\nSeleccione una opción: ");
}