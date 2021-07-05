using System.Collections.Generic;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.DataTypes
{
    public class BoardShape : IBoardShape
    {
        public IEnumerable<IBoardPosition> Positions { get; set; }
    }
}
