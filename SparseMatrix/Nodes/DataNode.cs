/// <summary>
///     Representa un nodo en una matriz dispersa representada por una lista enlazada.
/// </summary>
public abstract class DataNode: MatrixNode
{
    /// <summary>
    ///     La fila en la que se ubica el nodo.
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    ///     La columna en la que se ubica el nodo.
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    ///     Crea un nuevo nodo.
    /// </summary>
    /// <param name="value">El valor del nodo</param>
    /// <param name="row">La fila en la que se ubica el nodo</param>
    /// <param name="column">La columna en la que se ubica el nodo</param>
    public DataNode(int value, int row, int column) : base(value)
    {
        this.Row = row;
        this.Column = column;
    }
}