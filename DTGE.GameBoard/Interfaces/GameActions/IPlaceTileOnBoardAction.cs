using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.GameActions
{
    public interface IPlaceTileOnBoardAction : IIdentifiedAction
    {
        IBoard Board { get; }
        IBoardTile Tile { get; }
        IBoardPosition Position { get; }
    }
}
