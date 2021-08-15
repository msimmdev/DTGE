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
    public class RemoveObjectAction : IdentifiedAction, IRemoveObjectAction
    {
        public RemoveObjectAction(IBoardObject boardObject)
        {
            Object = boardObject;
        }

        public IBoardObject Object { get; private set; }

        public IGameDto GetDto()
        {
            return new RemoveObjectActionDto()
            {
                Id = Id.ToString(),
                Tags = Tags.ToList(),
                ObjectId = Object.Id.ToString()
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as RemoveObjectActionDto;
            Id = new Guid(objectData.Id);
            Tags = new HashSet<string>(objectData.Tags);
            Object = resolver.Resolve<IBoardObject>(new Guid(objectData.ObjectId));
        }

        public ValidationResult Validate()
        {
            if (Object.Board == null)
                return ValidationResult.NewError("Tile is not attached to board.");

            return ValidationResult.NewSuccess();
        }

        public void Execute(IEventHandler handler)
        {
            handler.Dispatch<ActionStartEvent<RemoveObjectAction>>(
                new ActionStartEvent<RemoveObjectAction>(this));

            PerformAction();

            handler.Dispatch<ActionEndEvent<RemoveObjectAction>>(
                new ActionEndEvent<RemoveObjectAction>(this));
        }

        private void PerformAction()
        {
            Object.Board.Tiles.Remove(Object.Id);
            Object.Board = null;
            Object.Position = null;
        }
    }
}
