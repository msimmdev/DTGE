using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class TilePathSerializationData : IGameSerializationData
    {
        public string Id { get; set; }
        public List<string> TileIds { get; set; }
        public int Distance { get; set; }
    }
}
