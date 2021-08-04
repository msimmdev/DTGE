using System;
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
            Tile = tile;
            NewPosition = newPosition;
        }

        public IBoardTile Tile { get; set; }

        public IBoardPosition NewPosition { get; set; }

        public IGameDto GetDto()
        {
            return new MoveTileActionDto()
            {
                Id = Id.ToString(),
                TileId = Tile.Id.ToString() ?? null,
                NewPosition = NewPosition.GetDto() as BoardPositionDto ?? null
            };
        }

        public void UseDto(IGameDto data, IObjectResolver resolver)
        {
            var objectData = data as MoveTileActionDto;
            Id = new Guid(objectData.Id);
            Tile = resolver.Resolve<IBoardTile>(new Guid(objectData.TileId));
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

        protected override void PerformAction()
        {
            Tile.Position = NewPosition;
        }
    }
}
