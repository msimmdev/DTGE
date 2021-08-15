using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.Utilities
{
    public static class BoardGenerator
    {
        public static IBoard GenerateBoardFromShape(IResolver resolver, IBoardShape shape)
        {
            var board = resolver.Create<IBoard>();
            foreach (var pos in shape.Positions)
            {
                var tile = resolver.Create<IBoardTile>();
                tile.Position = pos;
                tile.Board = board;
                board.Tiles.Add(tile.Id, tile);
            }
            return board;
        }
    }
}
