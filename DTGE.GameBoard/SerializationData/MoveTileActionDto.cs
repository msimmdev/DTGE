using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class MoveTileActionDto : IGameDto
    {
        public string Id { get; set; }
        public string TileId { get; set; }
        public BoardPositionDto NewPosition { get; set; }
    }
}
