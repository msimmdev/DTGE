using DTGE.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace DTGE.GameBoard.SerializationData
{
    public class TileGraphSerializationData : IGameSerializationData
    {
        public string Id { get; set; }
        public List<VertexSerializationData> Vertices { get; set; }
    }
}
