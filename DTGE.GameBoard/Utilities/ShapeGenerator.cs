using DTGE.GameBoard.DataTypes;
using System.Collections.Generic;

namespace DTGE.GameBoard.Utilities
{
    public static class ShapeGenerator
    {
        public static BoardShape GenerateRectangleShapeWithQuads(int maxX, int maxY)
        {
            var posList = new List<QuadBoardPosition>();
            for (var i = 0; i < maxX; i++)
            {
                for (var j = 0; j < maxY; j++)
                {
                    posList.Add(new QuadBoardPosition(i, j));
                }
            }
            return new BoardShape(posList);
        }

        public static BoardShape GeneratePerimeterShapeWithQuads(int maxX, int maxY)
        {
            var posList = new List<QuadBoardPosition>();
            for (var i = 0; i < maxX; i++)
            {
                for (var j = 0; j < maxY; j++)
                {
                    if (i == 0 || j == 0 || i == maxX - 1 || j == maxY - 1)
                        posList.Add(new QuadBoardPosition(i, j));
                }
            }

            return new BoardShape(posList);
        }
    }
}
