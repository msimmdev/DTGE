﻿using System.Collections.Generic;
using DTGE.Common.Interfaces;

namespace DTGE.GameBoard.SerializationData
{
    public class PlaceTileOnBoardActionDto : IGameDto
    {
        public string Id { get; set; }
        public List<string> Tags { get; set; }
        public string BoardId { get; set; }
        public string TileId { get; set; }
        public BoardPositionDto Position { get; set; }
    }
}
