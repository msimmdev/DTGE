using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class RemoveTileActionDto : IGameDto
    {
        public string Id { get; set; }
        public List<string> Tags { get; set; }
        public string TileId { get; set; }
    }
}
