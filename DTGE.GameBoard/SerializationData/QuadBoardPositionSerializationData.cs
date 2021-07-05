using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class QuadBoardPositionSerializationData : IGameSerializationData
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
