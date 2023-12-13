using System;
using System.IO;

class Videocentro
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("         *** Videocentro ***");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1. Modulo de Registro");
            Console.WriteLine("2. Modulo de Renta");
            Console.WriteLine("3. Modulo de Visualizacion");
            Console.WriteLine("4. Salir");
            Console.WriteLine("---------------------------------------");
            Console.Write("Seleccione una opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Registro();
                    break;
                case "2":
                    Renta();
                    break;
                case "3":
                    Visualizacion();
                    break;
                case "4":
                    Console.WriteLine();
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Gracias por utilizar este Sistema (='.'=)");
                    Console.ResetColor();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opcion invalida, intente de nuevo");
                    break;
            }
        }
    }

    static void Registro()
    {
        Console.WriteLine();
        Console.WriteLine("Ingrese los siguientes datos:");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("Apellidos: ");
        string apellidos = Console.ReadLine();
        Console.Write("Edad: ");
        int edad = int.Parse(Console.ReadLine());
        Console.Write("Sexo: ");
        string sexo = Console.ReadLine();
        Console.Write("CURP: ");
        string curp = Console.ReadLine();
        Console.Write("Telefono fijo: ");
        string telefonoFijo = Console.ReadLine();
        Console.Write("Telefono movil: ");
        string telefonoMovil = Console.ReadLine();
        Console.Write("Direccion: ");
        string direccion = Console.ReadLine();

        string idv = curp.Substring(0, 3).ToUpper() + new Random().Next(1000, 9999) + new Random().Next(0, 26).ToString("X") + new Random().Next(0, 26).ToString("X");
        string registro = idv + "," + nombre + "," + apellidos + "," + edad + "," + sexo + "," + curp + "," + telefonoFijo + "," + telefonoMovil + "," + direccion;

        using (StreamWriter file = new StreamWriter("usuarios.txt", true))
        {
            file.WriteLine(registro);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Usuario registrado con exito. Su identificador es: " + idv);
        Console.ReadLine();
        Console.ResetColor();
        Console.Clear();
    }

    static void Renta()
    {
        Console.Write("Ingrese su identificador IDV: ");
        string idv = Console.ReadLine();

        string[] usuarios = File.ReadAllLines("usuarios.txt");

        bool encontrado = false;
        string registroEncontrado = "";

        foreach (string registro in usuarios)
        {
            string[] datos = registro.Split(',');
            if (datos[0] == idv)
            {
                encontrado = true;
                registroEncontrado = registro;
                break;
            }
        }

        if (encontrado)
        {
            Console.Write("Ingrese el numero de peliculas a rentar (1-3): ");
            int cantidadPeliculas = int.Parse(Console.ReadLine());

            while (cantidadPeliculas < 1 || cantidadPeliculas > 3)
            {
                Console.Write("Numero de peliculas invalido. Ingrese el numero de peliculas a rentar (1-3): ");
                cantidadPeliculas = int.Parse(Console.ReadLine());
            }

            int[] peliculas = new int[cantidadPeliculas];

            for (int i = 0; i < cantidadPeliculas; i++)
            {
                Console.Write("Ingrese el identificador de la pelicula " + (i + 1) + " (1-10000): ");
                int pelicula = int.Parse(Console.ReadLine());

                while (pelicula < 1 || pelicula > 10000)
                {
                    Console.Write("Identificador de pelicula invalido. Ingrese el identificador de la pelicula " + (i + 1) + " (1-10000): ");
                    pelicula = int.Parse(Console.ReadLine());
                }

                peliculas[i] = pelicula;
            }

            string fechaDevolucion = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");
            string registroPrestamo = idv + "," + cantidadPeliculas + "," + fechaDevolucion;

            using (StreamWriter file = new StreamWriter("prestamos.txt", true))
            {
                file.WriteLine(registroPrestamo);
            }
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Peliculas rentadas con exito. Fecha de devolucion: " + fechaDevolucion);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Usuario no registrado");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void Visualizacion()
    {
        Console.WriteLine("---------------------------------------");
        Console.WriteLine("1. Usuarios Registrados");
        Console.WriteLine("2. Prestamos Realizados");
        Console.WriteLine("---------------------------------------");
        Console.Write("Seleccione una opcion: ");
        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                Console.Write("Ingrese la clave privada: ");
                string clavePrivadaUsuario = Console.ReadLine();
                if (clavePrivadaUsuario == "SECRETO")
                {
                    Console.WriteLine("---------------------------------------");
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Usuarios Registrados: ");
                    Console.ForegroundColor= ConsoleColor.DarkYellow;
                    string[] usuarios = File.ReadAllLines("usuarios.txt");
                    foreach (string usuario in usuarios)
                    {
                        Console.WriteLine(usuario);
                    }
                    Console.ResetColor();
                    Console.WriteLine("---------------------------------------");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Clave privada incorrecta");
                    Console.ResetColor();
                }
                break;
            case "2":
                Console.Write("Ingrese la clave privada: ");
                string clavePrivadaPrestamo = Console.ReadLine();
                if (clavePrivadaPrestamo == "SECRETO")
                {
                    Console.WriteLine("---------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Prestamos Realizados:");
                    Console.ForegroundColor= ConsoleColor.DarkYellow;
                    string[] prestamos = File.ReadAllLines("prestamos.txt");

                    foreach (string prestamo in prestamos)
                    {
                        Console.WriteLine(prestamo);
                    }
                    Console.ResetColor();
                    Console.WriteLine("---------------------------------------");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Clave privada incorrecta");
                    Console.ResetColor();
                }
                break;
            default:
                Console.WriteLine("Opcion invalida, intente de nuevo");
                break;
        }
        Console.ReadKey();
        Console.Clear();
    }
}
