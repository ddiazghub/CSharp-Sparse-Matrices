using System;
using System.Linq;

/// <summary>
///     Clase que representa una matriz dispersa representada por una lista enlazada.
/// </summary>
public abstract class SparseMatrix
{
    /// <summary>
    ///     Primer nodo de la lista.
    /// </summary>
    public MatrixNode Head { get; set; }

    /// <summary>
    ///     Número de filas de la matriz.
    /// </summary>
    public int N { get; protected set; }

    /// <summary>
    ///     Número de columnas de la matriz.
    /// </summary>
    public int M { get; protected set; }
    protected int max;

    /// <summary>
    ///     Convierte la matriz dispersa de vuelta a una matriz normal.
    /// </summary>
    /// <returns>La matriz convertida como string.</returns>
    public abstract string ToMatrix();

    /// <summary>
    ///     Representa la lista enlazada como una string.
    /// </summary>
    /// <returns>La lista como string.</returns>
    public override abstract string ToString();

    /// <summary>
    ///     Obtiene el tipo de matriz. Solo es para imprimir en consola.
    /// </summary>
    /// <returns>El tipo de matriz como string</returns>
    public string GetMatrixType()
    {
        if (this is DoublySparseMatrix)
            return "Lista doble";

        if (this is MultiSparseMatrix)
            return "Multilista";

        return "Lista simple";
    }

    /// <summary>
    ///     Devuelve la matriz suministrada como una string.
    /// </summary>
    /// <param name="matrix">La matriz que se va a convertir a string</param>
    /// <returns>La matriz como string</returns>
    protected string GetMatrixAsString(int[][] matrix)
    {
        string matrixString = "";

        foreach (int[] row in matrix)
        {
            matrixString += String.Format("|{0}|\n", String.Join(" ", row.Select(i => String.Format("{0," + (this.max.ToString().Length + 1) + "}", i.ToString()))));
        }

        return matrixString;
    }
}