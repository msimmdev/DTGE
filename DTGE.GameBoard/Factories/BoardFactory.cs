using DTGE.Common.Interfaces;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Factories
{
    public class BoardFactory : IGameFactory<IBoard>
    {
        public IBoard Get()
        {
            return new Board();
        }
    }
}
