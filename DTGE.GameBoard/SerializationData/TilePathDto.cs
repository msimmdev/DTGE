using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class TilePathDto : IGameDto
    {
        public string Id { get; set; }
        public List<string> TileIds { get; set; }
        public int Distance { get; set; }
    }
}
