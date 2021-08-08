using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.GameActions
{
    public interface IPlaceObjectOnBoardAction : IIdentifiedAction
    {
        IBoard Board { get; }
        IBoardObject Object { get; }
        IBoardPosition Position { get; }
    }
}
