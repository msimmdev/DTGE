using DTGE.Common.Interfaces;
using DTGE.GameBoard.Factories;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard
{
    public class FactoryManager
    {
        public FactoryManager()
        {
            BoardFactory = new BoardFactory();
            BoardTileFactory = new BoardTileFactory();
            BoardObjectFactory = new BoardObjectFactory();
        }

        public IGameFactory<IBoard> BoardFactory { get; set; }
        public IGameFactory<IBoardTile> BoardTileFactory { get; set; }
        public IGameFactory<IBoardObject> BoardObjectFactory { get; set; }
    }
}
