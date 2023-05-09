using System;

/// <summary>
///     Clase que representa una matriz representada por una multilista enlazada.
/// </summary>
public class MultiSparseMatrix : SparseMatrix
{
    /// <summary>
    ///     Crea una nueva matriz dispersa.
    /// </summary>
    /// <param name="matrix">La información de la matriz.</param>
    public MultiSparseMatrix(int[][] matrix)
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
                MatrixNode node = new MultiNode(matrix[i][j], j);

                // Si la lista está vacía
                if (this.Head == null)
                {
                    // Se inserta un nuevo nodo de fila en la cabeza de la lista y se añade el nodo anteriormente creado a la nueva fila
                    this.Head = new RowNode(i);
                    this.Head.Links[1] = node;
                    this.max = Math.Abs(node.Value);
                }
                else
                {
                    // Sino, se itera a través de la matriz hasta que se llegue a la fila correspondiente al nodo
                    RowNode rowNode = this.GetRow(i);

                    // Si la fila no existe
                    if (rowNode.Value > i && rowNode == this.Head)
                    {
                        // Se inserta un nuevo nodo de fila en la lista y se añade el nodo anteriormente creado a la nueva fila
                        RowNode newRow = new RowNode(i);
                        newRow.Links[0] = rowNode;
                        newRow.Links[1] = node;
                        this.Head = newRow;
                    }
                    else if (rowNode.Value != i)
                    {
                        // Se inserta un nuevo nodo de fila en la lista y se añade el nodo anteriormente creado a la nueva fila
                        RowNode newRow = new RowNode(i);
                        newRow.Links[0] = rowNode.Links[0];
                        rowNode.Links[0] = newRow;
                        newRow.Links[1] = node;
                    }
                    else
                    {
                        // Si la fila existe se itera a través de la fila hasta llegar a la column correspondiente al nodo
                        MultiNode current = rowNode.GetColumn(j);

                        //  Se inserta el nodo en la posición que diga su campo de columna.
                        if (((MultiNode) node).Column < current.Column)
                        {
                            rowNode.Links[1] = node;
                            node.Links[1] = current;
                        }
                        else if (((MultiNode) node).Column > current.Column)
                        {
                            node.Links[1] = current.Links[1];
                            current.Links[1] = node;
                        }
                    }
                }

                if (Math.Abs(node.Value) > this.max)
                    this.max = Math.Abs(node.Value);
            }
        }
    }

    /// <summary>
    ///     Obtiene el nodo de fila correspondiente al número de fila especificado.
    /// </summary>
    /// <param name="index">El índice de la fila</param>
    /// <returns>El nodo de fila correspondienet al número de fila especificado. Si no existe, se retorna el anterior nodo.</returns>
    public RowNode GetRow(int index)
    {
        RowNode current = (RowNode) this.Head;
            
        if (current == null)
            return null;

        while (current.Links[0] != null && current.Links[0].Value <= index)
            current = (RowNode) current.Links[0];

        return current;
    }

    public override string ToMatrix()
    {
        // Se crea una nueva matriz numérica
        int[][] matrix = new int[this.N][];

        for (int i = 0; i < this.N; i++)
            matrix[i] = new int[this.M];

        // Se empieza a iterar a través de las filas de la lista enlazada
        RowNode row = (RowNode) this.Head;

        // Mientras que todavía queden filas a través de las cuales iterar
        while (row != null)
        {
            // Se empieza a iterar a través de cada uno de los nodos que está en esa fila
            MultiNode current = (MultiNode) row.Links[1];
  
            // Mientras no se haya llegado al final de la lista se inserta el valor del nodo en donde digan los campos de fila y columna del nodo
            while (current != null)
            {
                matrix[current.Column][row.Value] = current.Value;

                // Se pasa al siguiente valor
                current = (MultiNode) current.Links[1];
            }

            // Se pasa a la siguiente fila
            row = (RowNode) row.Links[0];
        }

        return this.GetMatrixAsString(matrix);
    }

    public override string ToString()
    {
        string matrixString = "Estructura de los nodos fila: [fila]\nEstructura de los nodos de datos: [valor][columna]\n\n";

        RowNode row = (RowNode) this.Head;

        while (row != null)
        {
            matrixString += String.Format("[{0}] --> ", row.Value.ToString().PadLeft(this.N.ToString().Length, '0'));
            MultiNode node = (MultiNode) row.Links[1];

            while (node != null)
            {
                matrixString += String.Format("[{0}][{1}]", node.Value.ToString().PadLeft(this.N.ToString().Length, '0'), node.Column.ToString().PadLeft(this.N.ToString().Length, '0'));
                
                if (node.Links[1] != null)
                    matrixString += " --> ";

                node = (MultiNode) node.Links[1];
            }

            matrixString += "\n";

            if (row.Links[0] != null)
                matrixString += String.Format("{0}|\n{0}V\n", "".PadLeft(this.N.ToString().Length / 2));

            row = (RowNode) row.Links[0];
        }

        return matrixString;
    }
}