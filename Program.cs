using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    /// <summary>
    ///     Lista de matrices creadas.
    /// </summary>
    public static List<SparseMatrix> Matrixes { get; } = new List<SparseMatrix>();

    /// <summary>
    ///     Inicia la línea de comando y se añaden los comandos.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        CommandLineInterface cli = new CommandLineInterface();
        cli.HelpMenuTitle = "Los comandos disponibles son: ";

        Command singly = new Command("simple", "archivo");
        cli.AddCommand(singly);
        singly.Description = "Lee un archivo y crea una matriz dispersa basada en una lista enlazada simple.";
        singly.Action = args => Singly(args);

        Command doubly = new Command("doble", "archivo");
        cli.AddCommand(doubly);
        doubly.Description = "Lee un archivo y crea una matriz dispersa basada en una lista doblemente enlazada.";
        doubly.Action = args => Doubly(args);

        Command multi = new Command("multi", "archivo");
        cli.AddCommand(multi);
        multi.Description = "Lee un archivo y crea una matriz dispersa basada en una multilista enlazada.";
        multi.Action = args => Multi(args);

        Command list = new Command("listar");
        cli.AddCommand(list);
        list.Description = "Lista el número de matriz, las dimensiones y el tipo de lista con el cual están representadas todas las matrices almacenadas.";
        list.Action = args => List(args);

        Command show = new Command("mostrar", "n_matriz", "formato");
        cli.AddCommand(show);
        show.Description = "Imprime la matriz cuyo número sea n_matriz con el formato específicado. El formato puede ser cualquiera entre lista o matriz.";
        show.Action = args => Show(args);

        cli.Run();
    }

    /// <summary>
    ///     Lee la información de un archivo.
    /// </summary>
    /// <param name="filePath">La ruta del archivo</param>
    /// <returns>La información de la matriz de forma numérica.</returns>
    public static int[][] ReadMatrixFromFile(string filePath)
    {
        int[][] matrix = null;

        try
        {
            string separator = "";

            if (Path.GetExtension(filePath).Equals(".txt"))
                separator = " ";
            else if (Path.GetExtension(filePath).Equals(".csv"))
                separator = ",";
            else
            {
                Console.Error.WriteLine("El archivo debe ser .txt o .csv");
                
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);
            matrix = new int[lines.Length][];
            int m = lines[0].Split(separator).Length;

            for (int i = 0; i < lines.Length; i++)
            {
                matrix[i] = lines[i].Split(separator).Select(Int32.Parse).ToArray();

                if (matrix[i].Length != m)
                {
                    Console.Error.WriteLine("La matriz no es válida.");
                    return null;
                }
            }
        }
        catch (Exception)
        {
            Console.Error.WriteLine("No se pudo abrir el archivo. Es posible que no se encontró, está ocupado por otro programa o que el formato de la matriz sea inválido.");
        }

        return matrix;
    }

    /// <summary>
    ///     Crea una matriz dispersa basada en un lista enlazada del tipo especificado.
    /// </summary>
    /// <param name="type">Tipo de la matriz disperza</param>
    /// <param name="matrix">La matríz en forma numérica</param>
    /// <param name="n">Número filas de la matriz</param>
    /// <param name="m">Número de columnas de la matriz</param>
    public static void CreateMatrix(Type type, int[][] matrix)
    {
        try
        {
            object[] parameters = { matrix };

            if (matrix != null)
                Matrixes.Add((SparseMatrix) Activator.CreateInstance(type, parameters));
        }
        catch (IndexOutOfRangeException)
        {
            Console.Error.WriteLine("El tamaño de matriz especificado no es suficiente para contener a los elementos.");
        }
    }

    /// <summary>
    ///     Comando que crea una matriz dispersa basada en una lista enlazada simple.
    /// </summary>
    /// <param name="args">Argumentos del comando</param>
    public static void Singly(string[] args)
    {
        int[][] fields = ReadMatrixFromFile(args[0]);

        if (fields != null)
            CreateMatrix(typeof(SinglySparseMatrix), fields);
    }

    /// <summary>
    ///     Comando que crea una matriz dispersa basada en una lista enlazada doble.
    /// </summary>
    /// <param name="args">Argumentos del comando</param>
    public static void Doubly(string[] args)
    {
        int[][] fields = ReadMatrixFromFile(args[0]);

        if (fields != null)
            CreateMatrix(typeof(DoublySparseMatrix), fields);
    }

    /// <summary>
    ///     Comando que crea una matriz dispersa basada en una multilista enlazada.
    /// </summary>
    /// <param name="args">Argumentos del comando</param>
    public static void Multi(string[] args)
    {
        int[][] fields = ReadMatrixFromFile(args[0]);
        
        if (fields != null)
            CreateMatrix(typeof(MultiSparseMatrix), fields);
    }

    /// <summary>
    ///     Comando que lista el número de matriz, las dimensiones y el tipo de lista con el cual están representadas todas las matrices almacenadas.
    /// </summary>
    /// <param name="args">Argumentos del comando</param>
    public static void List(string[] args)
    {
        Console.WriteLine();

        for (int i = 0; i < Matrixes.Count; i++)
        {
            Console.WriteLine("[{0}]: Tamaño = [{1}x{2}], Tipo = {3}", i, Matrixes[i].N, Matrixes[i].M, Matrixes[i].GetMatrixType());
        }
    }

    /// <summary>
    ///     Imprime la matriz cuyo número sea n_matriz con el formato específicado. El formato puede ser cualquiera entre lista o matriz.
    /// </summary>
    /// <param name="args">Argumentos del comando</param>
    public static void Show(string[] args)
    {
        try
        {
            string matrixString = "";

            int i;

            try
            {
                i = Int32.Parse(args[0]);
            }
            catch
            {
                Console.Error.WriteLine("Argumentos inválidos.");
                return;
            }

            SparseMatrix matrix = Matrixes[i];

            switch (args[1])
            {
                case "matriz":
                    matrixString = matrix.ToMatrix();
                    break;
                
                case "lista":
                    matrixString = matrix.ToString();
                    break;

                default:
                    Console.Error.WriteLine("Formato inválido.");
                    return;
            }

            Console.WriteLine("[{0}]: Tamaño = [{1}x{2}], Tipo = {3}\n\nDatos:\n", i, matrix.N, matrix.M, matrix.GetMatrixType());
            Console.WriteLine(matrixString);
        }
        catch (IndexOutOfRangeException)
        {
            Console.Error.WriteLine("La matriz no existe.");
        }
    }
}
