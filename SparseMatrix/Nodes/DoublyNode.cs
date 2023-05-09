/// <summary>
///     Representa un nodo en una matriz dispersa representada por una lista doblemente enlazada.
/// </summary>
public class DoublyNode: DataNode
{
    /// <summary>
    ///     Crea un nuevo nodo.
    /// </summary>
    /// <param name="value">El valor del nodo</param>
    /// <param name="row">La fila en la que se ubica el nodo</param>
    /// <param name="column">La columna en la que se ubica el nodo</param>
    public DoublyNode(int value, int row, int column) : base(value, row, column)
    {
        this.Links.Add(null);
        this.Links.Add(null);
    }
}