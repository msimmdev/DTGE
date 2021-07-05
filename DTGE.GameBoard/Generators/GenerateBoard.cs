using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.Generators;

namespace DTGE.GameBoard.Generators
{
    public class GenerateBoard : IGenerateBoard
    { 
        public GenerateBoard()
        {
            FactoryManager = new FactoryManager();
        }

        public FactoryManager FactoryManager { get; set; }

        public IBoard FromShape(IBoardShape shape)
        {
            var board = FactoryManager.BoardFactory.Get();
            foreach (var pos in shape.Positions)
            {
                GetOrCreateTile(board, pos, shape);
            }
            return board;
        }

        private IBoardTile GetOrCreateTile(IBoard board, IBoardPosition pos, IBoardShape shape)
        {
            if (board.HasTile(pos))
            {
                return board.FindTile(pos);
            }

            var newTile = FactoryManager.BoardTileFactory.Get();
            board.Tiles.Add(newTile.Id, newTile);
            newTile.Position = pos;
            newTile.Board = board;

            return newTile;
        }
    }
}
