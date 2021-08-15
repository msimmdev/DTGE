using System;
using System.Collections.Generic;
using System.Linq;
using DTGE.Common.Base;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.GameEvents;
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
                ObjectId = Object.Id.ToString(),
                NewPosition = NewPosition.GetDto() as BoardPositionDto,
                Tags = Tags.ToList()
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as MoveObjectActionDto;
            Id = new Guid(objectData.Id);
            Object = resolver.Resolve<IBoardObject>(new Guid(objectData.ObjectId));
            NewPosition = resolver.Create<IBoardPosition>(objectData.NewPosition);
            Tags = new HashSet<string>(objectData.Tags);
        }

        public ValidationResult Validate()
        {
            if (Object.Board == null)
                return ValidationResult.NewError("Object is not attached to a board.");

            if (!Object.Board.HasTile(NewPosition))
                return ValidationResult.NewError("There is no tile at requested position.");

            return ValidationResult.NewSuccess();
        }

        public void Execute(IEventHandler handler)
        {
            handler.Dispatch(
                new ActionStartEvent<MoveObjectAction>(this));

            PerformAction(handler);

            handler.Dispatch(
                new ActionEndEvent<MoveObjectAction>(this));
        }

        private void PerformAction(IEventHandler handler)
        {
            Object.Position = NewPosition;

            handler.Dispatch(new ObjectMovesToTileEvent(
                Object,
                Object.Board.FindTile(NewPosition)
            ));
        }
    }
}
