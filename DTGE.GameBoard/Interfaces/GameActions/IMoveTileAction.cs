using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.GameActions
{
    public interface IMoveTileAction : IIdentifiedAction
    {
        IBoardTile Tile { get; }
        IBoardPosition NewPosition { get; }
    }
}
