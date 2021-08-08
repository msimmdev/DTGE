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
    public class MoveObjectAction : IdentifiedAction, IMoveObjectAction
    {
        public MoveObjectAction(IBoardObject boardObject, IBoardPosition newPosition)
        {
            Id = Guid.NewGuid();
            Object = boardObject;
            NewPosition = newPosition;
        }

        public IBoardObject Object { get; private set; }

        public IBoardPosition NewPosition { get; private set; }

        public IGameDto GetDto()
        {
            return new MoveObjectActionDto()
            {
                Id = Id.ToString(),
                ObjectId = Object.Id.ToString() ?? null,
                NewPosition = NewPosition.GetDto() as BoardPositionDto ?? null,
                Tags = Tags.ToList()
            };
        }

        public void UseDto(IGameDto data, IObjectResolver resolver)
        {
            var objectData = data as MoveObjectActionDto;
            Id = new Guid(objectData.Id);
            Object = resolver.Resolve<IBoardObject>(new Guid(objectData.ObjectId));
            NewPosition = resolver.Create<IBoardPosition>(objectData.NewPosition);
            Tags = new HashSet<string>(objectData.Tags);
        }

        public ValidationResult Validate()
        {
            if (Object == null)
                return ValidationResult.NewError("Tile is required for MoveTile action.");

            if (NewPosition == null)
                return ValidationResult.NewError("NewPosition is required for PlaceTileOnBoard action.");

            if (Object.Board == null)
                return ValidationResult.NewError("Tile is not attached to a board.");

            if (Object.Board.HasTile(NewPosition))
                return ValidationResult.NewError("A tile already exists at the given position.");

            return ValidationResult.NewSuccess();
        }

        protected override void PerformAction()
        {
            Object.Position = NewPosition;
        }
    }
}
