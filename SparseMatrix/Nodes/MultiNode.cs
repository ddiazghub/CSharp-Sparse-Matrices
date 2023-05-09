
/// <summary>
///     Un nodo de una matriz dispersa representada por una multilista.
/// </summary>
public class MultiNode : MatrixNode
{
    /// <summary>
    ///     La columna en la cual se ubica el nodo.
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    ///     Crea un nuevo nodo.
    /// </summary>
    /// <param name="value">El valor del nodo.</param>
    /// <param name="column">La columna en la cual se ubica el nodo.</param>
    public MultiNode(int value, int column) : base(value)
    {
        this.Column = column;
        this.Links.Add(null);
        this.Links.Add(null);
    }
}