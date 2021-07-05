using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class BoardTileSerializationData : IGameSerializationData
    {
        public string Id { get; set; }
        public string BoardId { get; set; }
        public IGameSerializationData Position { get; set; }
    }
}
