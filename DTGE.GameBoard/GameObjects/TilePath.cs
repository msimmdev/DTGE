using System;
using System.Collections.Generic;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.GameObjects
{
    public class TilePath : IdentifiedObject, ITilePath
    {
        public TilePath() : base()
        {
            Tiles = new List<IBoardTile>();
        }

        public IList<IBoardTile> Tiles { get; set; }
        public int Distance { get; set; }
        public Func<Guid, IBoardTile> ObjectResolver { get; set; }

        public IGameDto GetDto()
        {
            var tileIds = new List<string>();

            foreach (var tile in Tiles)
            {
                tileIds.Add(tile.Id.ToString());
            }

            return new TilePathDto()
            {
                Id = Id.ToString(),
                TileIds = tileIds,
                Distance = Distance
            };
        }

        public void UseDto(IGameDto data, IObjectResolver resolver)
        {
            var objectData = data as TilePathDto;
            Id = new Guid(objectData.Id);
            Distance = objectData.Distance;
            foreach (var tileId in objectData.TileIds)
            {
                var tile = ObjectResolver(new Guid(tileId));
                Tiles.Add(tile);
            }
        }
    }
}
