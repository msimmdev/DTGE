using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class BoardPositionDto : IGameDto
    {
        public string Type { get; set; }
        public string Position { get; set; }
    }
}
