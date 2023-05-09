using System.Collections.Generic;
using System;

/// <summary>
///     Interfaz de línea de comandos que lee continuamente entrada del usuario y es capaz de ejecutar un grupo de comandos establecidaos.
/// </summary>
public class CommandLineInterface
{
    /// <summary>
    ///     Título que se va a mostrar cuando se muestre el menu de ayuda.
    /// </summary>
    public string HelpMenuTitle { get; set; } = "";

    /// <summary>
    ///     Un diccionario que almacena los comandos y permite buscarlos rápidamente por el nombre.
    /// </summary>
    public Dictionary<string, Command> Commands { get; }

    /// <summary>
    ///     Crea una nueva interfaz de línea de comandos con 2 comandos por defecto, ayuda y salir.
    /// </summary>
    public CommandLineInterface()
    {
        this.Commands = new Dictionary<string, Command>();
        Command help = new Command("ayuda");
        help.Description = "Muestra este menú.";
        help.Action = (args) => this.Help();
        this.AddCommand(help);

        Command exit = new Command("salir");
        exit.Description = "Termina la ejecución del programa.";
        exit.Action = (args) => Environment.Exit(0);
        this.AddCommand(exit);
    }

    /// <summary>
    ///     Imprime a consola un menú de ayuda.
    /// </summary>
    private void Help() {
		Console.WriteLine(this.HelpMenuTitle + "\n");

        foreach (Command command in this.Commands.Values)
            Console.WriteLine(command.ToString());

        Console.WriteLine();
	}

    /// <summary>
    ///     Añade un nuevo commando.
    /// </summary>
    /// <param name="command">La referencia al comando a añadir.</param>
    public void AddCommand(Command command)
    {
        if (command.Name.Length > 20)
        {
            Console.Error.WriteLine("El comando no puede tener una extensión mayor a 20 caracteres");
            return;
        }

        if (this.Commands.ContainsKey(command.Name))
        {
            Console.Error.WriteLine("El comando ya existe");
            return;
        }

        this.Commands.Add(command.Name, command);
    }

    /// <summary>
    ///     Elimina un comando existente.
    /// </summary>
    /// <param name="command">El nombre del comando a eliminar.</param>
    public void RemoveCommand(string command)
    {
        if (!this.Commands.Remove(command))
            Console.Error.WriteLine("El comando no existe");
    }

    /// <summary>
    ///     Ejecuta la interfaz de línea de comandos.
    /// </summary>
    public void Run() {
		this.Help();

        while (true)
        {
            Console.Write("$ ");
            string[] input = Console.ReadLine().Split(" ");

            if (input[0].Length == 0)
                continue;

            string command = input[0];
            string[] args = new ArraySegment<String>(input, 1, input.Length - 1).ToArray();

            try
            {
                this.Commands[command].Execute(args);
            }
            catch (KeyNotFoundException)
            {
                Console.Error.WriteLine("El comando no existe.");
            }
        }
    }
}