using DTGE.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DTGE.GameBoard.SerializationData
{
    public class TileGraphDto : IGameDto
    {
        public string Id { get; set; }
        public List<VertexDto> Vertices { get; set; }
    }
}
