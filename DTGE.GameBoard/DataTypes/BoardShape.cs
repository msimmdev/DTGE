using System.Collections.Generic;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.DataTypes
{
    public class BoardShape : IBoardShape
    {
        public BoardShape(IEnumerable<IBoardPosition> positions)
        {
            Positions = positions;
        }

        public IEnumerable<IBoardPosition> Positions { get; }
    }
}
