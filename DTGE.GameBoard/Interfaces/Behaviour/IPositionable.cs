using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Interfaces.Behaviour
{
    /// <summary>
    /// Somethjing that can be assigned to a position on a board.
    /// </summary>
    public interface IPositionable
    {
        /// <summary>
        /// The position of the item.
        /// </summary>
        IBoardPosition Position { get; set; }
    }
}
