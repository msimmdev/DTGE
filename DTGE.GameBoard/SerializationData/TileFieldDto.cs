using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class TileFieldDto : IGameDto
    {
        public string Id { get; set; }
        public List<string> TileIds { get; set; }
    }
}
