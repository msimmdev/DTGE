using System;
using System.Collections.Generic;
using System.Linq;
using DTGE.Common.Base;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.GameActions
{
    public class MoveTileAction : IdentifiedAction, IMoveTileAction
    {
        public MoveTileAction(IBoardTile tile, IBoardPosition newPosition)
        {
            Id = Guid.NewGuid();
            Tile = tile;
            NewPosition = newPosition;
        }

        public IBoardTile Tile { get; private set; }

        public IBoardPosition NewPosition { get; private set; }

        public IGameDto GetDto()
        {
            return new MoveTileActionDto()
            {
                Id = Id.ToString(),
                TileId = Tile.Id.ToString() ?? null,
                NewPosition = NewPosition.GetDto() as BoardPositionDto ?? null,
                Tags = Tags.ToList()
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as MoveTileActionDto;
            Id = new Guid(objectData.Id);
            Tile = resolver.Resolve<IBoardTile>(new Guid(objectData.TileId));
            NewPosition = resolver.Create<IBoardPosition>(objectData.NewPosition);
            Tags = new HashSet<string>(objectData.Tags);
        }

        public ValidationResult Validate()
        {
            if (Tile == null)
                return ValidationResult.NewError("Tile is required for MoveTile action.");

            if (NewPosition == null)
                return ValidationResult.NewError("NewPosition is required for PlaceTileOnBoard action.");

            if (Tile.Board == null)
                return ValidationResult.NewError("Tile is not attached to a board.");

            if (Tile.Board.HasTile(NewPosition))
                return ValidationResult.NewError("A tile already exists at the given position.");

            return ValidationResult.NewSuccess();
        }

        public void Execute(IEventHandler handler)
        {
            handler.Dispatch<ActionStartEvent<MoveTileAction>>(
                new ActionStartEvent<MoveTileAction>(this));

            PerformAction();

            handler.Dispatch<ActionEndEvent<MoveTileAction>>(
                new ActionEndEvent<MoveTileAction>(this));
        }

        private void PerformAction()
        {
            Tile.Position = NewPosition;
        }
    }
}
