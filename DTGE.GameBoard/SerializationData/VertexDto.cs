using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class VertexDto : IGameDto
    {
        public string ObjectId { get; set; }
        public List<EdgeDto> Edges { get; set; }
    }
}