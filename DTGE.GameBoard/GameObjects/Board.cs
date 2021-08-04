using System;
using System.Linq;
using System.Collections.Generic;
using DTGE.Common.Base;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.GameObjects
{
    public class Board : IdentifiedObject, IBoard
    {
        public Board() : base()
        {
            Tiles = new Dictionary<Guid, IBoardTile>();
            Objects = new Dictionary<Guid, IBoardObject>();
        }

        public IDictionary<Guid, IBoardTile> Tiles { get; set; }
        public IDictionary<Guid, IBoardObject> Objects { get; set; }

        public IBoardTile FindTile(IBoardPosition position)
        {
            return Tiles.Values.Where(t => t.Position.Equals(position)).Single();
        }

        public bool HasTile(IBoardPosition position)
        {
            return Tiles.Values.Any(t => t.Position.Equals(position));
        }

        public IGameDto GetDto()
        {
            return new BoardDto()
            {
                Id = this.Id.ToString(),
                BoardTileIds = this.Tiles.Keys.Select(g => g.ToString()).ToList(),
                ObjectIds = this.Objects.Keys.Select(g => g.ToString()).ToList()
            };
        }

        public void UseDto(IGameDto data, IObjectResolver resolver)
        {
            var dataObject = data as BoardDto;
            Id = new Guid(dataObject.Id);
        }
    }
}
