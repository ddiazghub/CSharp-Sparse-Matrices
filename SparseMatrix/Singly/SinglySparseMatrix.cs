using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Clase que representa una matriz dispersa representada por una lista enlazada simple.
/// </summary>
public class SinglySparseMatrix: SparseMatrix
{
    /// <summary>
    ///     El último nodo de la lista.
    /// </summary>
    public SinglyNode Tail {
        get
        {
            SinglyNode current = (SinglyNode) this.Head;
            
            if (current == null)
                return null;

            while (current.Links[0] != null)
                current = (SinglyNode) current.Links[0];

            return current;
        }
    }

    /// <summary>
    ///     Crea una nueva matriz representada por una lista enlazada simple.
    /// </summary>
    /// <param name="matrix">La matriz</param>
    public SinglySparseMatrix(int[][] matrix)
    {
        this.N = matrix.Length;
        this.M = matrix[0].Length;

        // Se itera a través de la matriz numérica
        for (int i = 0; i < this.N; i++)
        {
            for (int j = 0; j < this.M; j++)
            {
                // Si el valor es 0, no se inserta en la lista.
                if (matrix[i][j] == 0)
                    continue;
                    
                // Si no es 0, se crea un nuevo nodo
                MatrixNode node = new SinglyNode(matrix[i][j], i, j);

                // Si la lista está vacía
                if (this.Head == null)
                {
                    // Se inserta el nodo en la cabeza de la lista
                    this.Head = node;
                    this.max = Math.Abs(this.Head.Value);
                }
                else
                {
                    // Sino, se inserta el nodo al final de la lista
                    if (Math.Abs(node.Value) > this.max)
                        this.max = Math.Abs(node.Value);

                    this.Tail.Links[0] = node;
                }
            }
        }
    }

    public override string ToMatrix()
    {
        // Se crea una nueva matriz numérica
        int[][] matrix = new int[this.N][];

        for (int i = 0; i < this.N; i++)
            matrix[i] = new int[this.M];

        // Se itera a través de la lista
        SinglyNode node = (SinglyNode) this.Head;

        // Mientras no se haya llegado al final de la lista se inserta el valor del nodo en donde digan los campos de fila y columna del nodo
        while (node != null)
        {
            matrix[node.Row][node.Column] = node.Value;
            node = (SinglyNode) node.Links[0];
        }

        return this.GetMatrixAsString(matrix);
    }

    public override string ToString()
    {
        string matrixString = "Estructura de los nodos: [valor][fila][columna]\n\n";

        SinglyNode node = (SinglyNode) this.Head;

        while (node != null)
        {
            matrixString += String.Format("[{0}][{1}][{2}]", node.Value, node.Row, node.Column);

            if (node.Links[0] != null)
                matrixString += " --> ";

            node = (SinglyNode) node.Links[0];
        }

        return matrixString;
    }
}