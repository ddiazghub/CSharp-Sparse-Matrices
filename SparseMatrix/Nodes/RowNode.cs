/// <summary>
///     Un nodo que representa una fila en una matriz dispersa representada por una multilista.
/// </summary>
/// 
public class RowNode : MatrixNode
{
    /// <summary>
    ///     Último nodo de la fila
    /// </summary>
    public MultiNode Tail
    {
        get
        {
            MultiNode current = (MultiNode) this.Links[0];
            
            if (current == null)
                return null;

            while (current.Links[0] != null)
                current = (MultiNode) current.Links[0];

            return current;
        }
    }

    /// <summary>
    ///     Crea un nuevo nodo fila
    /// </summary>
    /// <param name="value">El número de fila</param>
    public RowNode(int value) : base(value)
    {
        this.Links.Add(null);
        this.Links.Add(null);
    }

    /// <summary>
    ///     Obtiene el nodo ubicado en la columna especificada.
    /// </summary>
    /// <param name="index">El índice de la columna a buscar</param>
    /// <returns>El nodo en la columna especificada, si la columna no existe, retorna el nodo de la columna anterior.</returns>
    public MultiNode GetColumn(int index)
    {
        MultiNode current = (MultiNode) this.Links[1];
            
        if (current == null)
            return null;

        while (current.Links[0] != null && ((MultiNode) current.Links[1]).Column < index)
            current = (MultiNode) current.Links[1];

        return current;
    }
}