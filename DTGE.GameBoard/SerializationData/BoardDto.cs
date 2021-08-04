using System;
using System.Linq;
using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class BoardDto : IGameDto
    {
        public string Id { get; set; }
        public List<string> BoardTileIds { get; set; }
        public List<string> ObjectIds { get; set; }
    }
}
