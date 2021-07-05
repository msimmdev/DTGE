using DTGE.Common.Interfaces;
using DTGE.GameBoard.GameObjects;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Factories
{
    public class BoardTileFactory : IGameFactory<IBoardTile>
    {
        public IBoardTile Get()
        {
            return new BoardTile();
        }
    }
}
