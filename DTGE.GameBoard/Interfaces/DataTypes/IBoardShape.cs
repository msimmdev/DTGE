using System.Collections.Generic;

namespace DTGE.GameBoard.Interfaces.DataTypes
{
    /// <summary>
    /// A type that describes a collection of board positions in a specific shape.
    /// </summary>
    public interface IBoardShape
    {
        IEnumerable<IBoardPosition> Positions { get; set; }
    }
}
