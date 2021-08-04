using System;
using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class BoardObjectDto : IGameDto
    { 
        public string Id { get; set; }
        public string BoardId { get; set; }
        public BoardPositionDto Position { get; set; }
    }
}
