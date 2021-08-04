using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.GameActions
{
    public interface IRemoveTileAction : IIdentifiedAction
    {
        IBoardTile Tile { get; }
    }
}
