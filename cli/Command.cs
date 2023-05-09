using System.Collections.Generic;
using System;

/// <summary>
///     Clase que representa un comando de una interfaz de línea de comandos.
/// </summary>
public class Command
{
    /// <summary>
    ///     Nombre del comando (O el comando en sí).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Lista de argumentos que el comando acepta.
    /// </summary>
    public string[] Args { get; set; }

    /// <summary>
    ///     Breve descripción del comando.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     El método que el comando ejecuta. Contiene el procedimiento que se va a ejecutar cuando se llame el comando.
    /// </summary>
    public Action<string[]> Action { get; set; }

    /// <summary>
    ///     Crea un nuevo comando.
    /// </summary>
    /// <param name="name">El nombre del comando.</param>
    /// <param name="args">El nombre de los parámetros que el comando acepta.</param>
    public Command(string name, params string[] args)
    {
        this.Args = args;
        this.Name = name;
        this.Description = "";
        this.Action = args => { };
    }

    /// <summary>
    ///     Ejecuta el comando.
    /// </summary>
    /// <param name="args">Los argumentos a pasarle al comando.</param>
    public void Execute(params string[] args)
    {
        if (args.Length != this.Args.Length)
            Console.WriteLine("Número inválido de argumentos");
        else
            this.Action(args);
    }

    /// <summary>
    ///     Exporta el comando como string con nombre, parámetros y descripción.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string command = "$ " + this.Name;

        if (this.Args.Length > 0)
            command += " <" + String.Join("> <", this.Args) + ">";

        return String.Format("{0,-30}  - {1}", command, this.Description);
    }
}