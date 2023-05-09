using System.Collections.Generic;

/// <summary>
///     Representa un nodo en una matriz dispersa representada por una lista enlazada.
/// </summary>
public abstract class MatrixNode
{
    /// <summary>
    ///     El valor del nodo.
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    ///     Los enlaces que el nodo tiene con otros nodos de la lista.
    /// </summary>
    public List<MatrixNode> Links { get; }

    /// <summary>
    ///     Crea un nuevo nodo.
    /// </summary>
    /// <param name="value">El valor del nodo.</param>
    public MatrixNode(int value)
    {
        this.Value = value;
        this.Links = new List<MatrixNode>();
    }
}