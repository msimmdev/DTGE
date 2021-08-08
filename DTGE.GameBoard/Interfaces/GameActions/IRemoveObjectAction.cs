using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Interfaces.GameActions
{
    public interface IRemoveObjectAction : IIdentifiedAction
    {
        IBoardObject Object { get; }
    }
}
