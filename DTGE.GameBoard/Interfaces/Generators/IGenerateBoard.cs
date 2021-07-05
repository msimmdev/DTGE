using DTGE.Common.Interfaces;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.Generators
{
    /// <summary>
    /// A set of functions that can generate boards.
    /// </summary>
    public interface IGenerateBoard
    {
        FactoryManager FactoryManager { get; set; }

        /// <summary>
        /// Given a shape this generates a board with tiles in positions corresponding to the shape.
        /// </summary>
        /// <param name="shape">A shape to base the board generation on.</param>
        /// <returns>A board with tiles in the positions corresponding to the shape.</returns>
        IBoard FromShape(IBoardShape shape);
    }
}
