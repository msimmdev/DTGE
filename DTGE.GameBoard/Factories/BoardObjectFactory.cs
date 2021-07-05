using DTGE.Common.Interfaces;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Factories
{
    public class BoardObjectFactory : IGameFactory<IBoardObject>
    {
        public IBoardObject Get()
        {
            return new BoardObject();
        }
    }
}
