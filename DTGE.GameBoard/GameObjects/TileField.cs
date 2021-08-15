using System;
using System.Collections.Generic;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.GameObjects
{
    /// <summary>
    /// An unordered set of tiles.
    /// </summary>
    public class TileField : IdentifiedObject, ITileField
    {
        public TileField() : base()
        {
            Tiles = new HashSet<IBoardTile>();
        }

        public ISet<IBoardTile> Tiles { get; set; }
        public Func<Guid, IBoardTile> ObjectResolver { get; set; }

        public IGameDto GetDto()
        {
            var tileIds = new List<string>();
            foreach (var tile in Tiles)
            {
                tileIds.Add(tile.Id.ToString());
            }

            return new TileFieldDto()
            {
                Id = Id.ToString(),
                TileIds = tileIds
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as TileFieldDto;
            Id = new Guid(objectData.Id);
            foreach (var tileId in objectData.TileIds)
            {
                var tile = ObjectResolver(new Guid(tileId));
                Tiles.Add(tile);
            }
        }
    }
}
