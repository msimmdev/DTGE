using System;
using System.Collections.Generic;
using System.Linq;
using DTGE.Common.Core;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.GameActions
{
    public class PlaceTileOnBoardAction : IdentifiedAction, IPlaceTileOnBoardAction
    {
        public PlaceTileOnBoardAction(IBoard board, IBoardTile tile, IBoardPosition position)
        {
            Board = board;
            Tile = tile;
            Position = position;
        }

        public IBoard Board { get; private set; }
        public IBoardTile Tile { get; private set; }
        public IBoardPosition Position { get; private set; }

        public IGameDto GetDto()
        {
            return new PlaceTileOnBoardActionDto()
            {
                Id = Id.ToString(),
                Tags = Tags.ToList(),
                TileId = Tile.Id.ToString(),
                BoardId = Board.Id.ToString(),
                Position = Position.GetDto() as BoardPositionDto
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as PlaceTileOnBoardActionDto;
            Id = new Guid(objectData.Id);
            Tags = new HashSet<string>(objectData.Tags);
            Tile = resolver.Resolve<IBoardTile>(new Guid(objectData.TileId));
            Board = resolver.Resolve<IBoard>(new Guid(objectData.BoardId));
            Position = resolver.Create<IBoardPosition>(objectData.Position);
        }

        public ValidationResult Validate()
        {
            if (Tile == null)
                return ValidationResult.NewError("Tile is required for PlaceTileOnBoard action.");

            if (Board == null)
                return ValidationResult.NewError("Board is required for PlaceTileOnBoard action.");

            if (Position == null)
                return ValidationResult.NewError("Position is required for PlaceTileOnBoard action.");

            if (Tile.Board != null)
                return ValidationResult.NewError("Tile is already attached to a board.");

            if (Tile.Position != null)
                return ValidationResult.NewError("Tile is already has a position.");

            if (Board.HasTile(Position))
                return ValidationResult.NewError("A Tile already exists at this position.");


            return ValidationResult.NewSuccess();
        }

        public void Execute(IEventHandler handler)
        {
            handler.Dispatch<ActionStartEvent<PlaceTileOnBoardAction>>(
                new ActionStartEvent<PlaceTileOnBoardAction>(this));

            PerformAction();

            handler.Dispatch<ActionEndEvent<PlaceTileOnBoardAction>>(
                new ActionEndEvent<PlaceTileOnBoardAction>(this));
        }

        private void PerformAction()
        {
            Tile.Position = Position;
            Tile.Board = Board;
            Board.Tiles.Add(Tile.Id, Tile);
        }
    }
}
