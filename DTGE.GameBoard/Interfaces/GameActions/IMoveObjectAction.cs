using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.GameActions
{
    public interface IMoveObjectAction : IIdentifiedAction
    {
        IBoardObject Object { get; }
        IBoardPosition NewPosition { get; }
    }
}
