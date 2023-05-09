/// <summary>
///     Un nodo de una matriz dispersa representada por una lista enlazada simple.
/// </summary>
public class SinglyNode: DataNode
{
    /// <summary>
    ///     Crea un nuevo nodo.
    /// </summary>
    /// <param name="value">El valor del nodo</param>
    /// <param name="row">La fila en la que está ubicado</param>
    /// <param name="column">La columna en la que está ubicado</param>
    public SinglyNode(int value, int row, int column) : base(value, row, column)
    {
        this.Links.Add(null);
    }
}