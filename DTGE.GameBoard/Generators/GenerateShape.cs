using System.Collections.Generic;
using DTGE.GameBoard.DataTypes;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.Generators;

namespace DTGE.GameBoard.Generators
{
    public class GenerateShape : IGenerateShape
    {
        public IBoardShape Rectangle(int xWidth, int yHeight)
        {
            var posSet = new HashSet<IBoardPosition>();
            var adjSet = new Dictionary<IBoardPosition, IEnumerable<IBoardPosition>>();

            for (var i = 1; i <= xWidth; i++)
            {
                for (var j = 1; j <= yHeight; j++)
                {
                    var pos = new QuadBoardPosition(i, j);
                    posSet.Add(pos);
                }
            }

            return new BoardShape()
            {
                Positions = posSet,
            };
        }
    }
}
