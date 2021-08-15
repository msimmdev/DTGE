using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.GameActions
{
    public interface IMoveObjectWithPathAction : IIdentifiedAction
    {
        IBoardObject Object { get; }
        ITilePath Path { get; }
    }
}
