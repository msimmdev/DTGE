using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Interfaces.Generators
{
    /// <summary>
    /// A set of functions that can generate board shapes.
    /// </summary>
    public interface IGenerateShape
    {
        /// <summary>
        /// Generates a board shape in a rectangle with given dimensions.
        /// </summary>
        /// <param name="xWidth">The width of the rectangle</param>
        /// <param name="yHeight">The height of the rectangle</param>
        /// <returns>A shape in a rectangular configuration</returns>
        IBoardShape Rectangle(int xWidth, int yHeight);
    }
}
