using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class VertexSerializationData : IGameSerializationData
    {
        public string ObjectId { get; set; }
        public List<EdgeSerializationData> Edges { get; set; }
    }
}