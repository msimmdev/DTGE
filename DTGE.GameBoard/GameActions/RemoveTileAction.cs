using System;
using System.Collections.Generic;
using System.Linq;
using DTGE.Common.Base;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.GameActions
{
    public class RemoveTileAction : IdentifiedAction, IRemoveTileAction
    {
        public RemoveTileAction(IBoardTile tile)
        {
            Tile = tile;
        }

        public IBoardTile Tile { get; private set; }

        public IGameDto GetDto()
        {
            return new RemoveTileActionDto()
            {
                Id = Id.ToString(),
                Tags = Tags.ToList(),
                TileId = Tile.Id.ToString()
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as RemoveTileActionDto;
            Id = new Guid(objectData.Id);
            Tags = new HashSet<string>(objectData.Tags);
            Tile = resolver.Resolve<IBoardTile>(new Guid(objectData.TileId));
        }

        public ValidationResult Validate()
        {
            if (Tile.Board == null)
                return ValidationResult.NewError("Tile is not attached to board.");

            return ValidationResult.NewSuccess();
        }

        public void Execute(IEventHandler handler)
        {
            handler.Dispatch<ActionStartEvent<RemoveTileAction>>(
                new ActionStartEvent<RemoveTileAction>(this));

            PerformAction();

            handler.Dispatch<ActionEndEvent<RemoveTileAction>>(
                new ActionEndEvent<RemoveTileAction>(this));
        }

        private void PerformAction()
        {
            Tile.Board.Tiles.Remove(Tile.Id);
            Tile.Board = null;
            Tile.Position = null;
        }
    }
}
