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
using DTGE.GameBoard.GameEvents;

namespace DTGE.GameBoard.GameActions
{
    public class PlaceObjectOnBoardAction : IdentifiedAction, IPlaceObjectOnBoardAction
    {
        public PlaceObjectOnBoardAction(IBoard board, IBoardObject boardObject, IBoardPosition position)
        {
            Board = board;
            Object = boardObject;
            Position = position;
        }

        public IBoard Board { get; private set; }
        public IBoardObject Object { get; private set; }
        public IBoardPosition Position { get; private set; }

        public IGameDto GetDto()
        {
            return new PlaceObjectOnBoardActionDto()
            {
                Id = Id.ToString(),
                Tags = Tags.ToList(),
                ObjectId = Object.Id.ToString(),
                BoardId = Board.Id.ToString(),
                Position = Position.GetDto() as BoardPositionDto
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as PlaceObjectOnBoardActionDto;
            Id = new Guid(objectData.Id);
            Tags = new HashSet<string>(objectData.Tags);
            Object = resolver.Resolve<IBoardObject>(new Guid(objectData.ObjectId));
            Board = resolver.Resolve<IBoard>(new Guid(objectData.BoardId));
            Position = resolver.Create<IBoardPosition>(objectData.Position);
        }

        public ValidationResult Validate()
        {
            if (Object.Board != null)
                return ValidationResult.NewError("Object is already attached to a board.");

            if (Object.Position != null)
                return ValidationResult.NewError("Object already has a position.");

            if (!Board.HasTile(Position))
                return ValidationResult.NewError("There is no tile at the requested position.");


            return ValidationResult.NewSuccess();
        }

        public void Execute(IEventHandler handler)
        {
            handler.Dispatch(
                new ActionStartEvent<PlaceObjectOnBoardAction>(this));

            PerformAction(handler);

            handler.Dispatch(
                new ActionEndEvent<PlaceObjectOnBoardAction>(this));
        }

        private void PerformAction(IEventHandler handler)
        {
            Object.Position = Position;
            Object.Board = Board;
            Board.Objects.Add(Object.Id, Object);

            handler.Dispatch(new ObjectPlacedOnTileEvent(
                Object,
                Board.FindTile(Position)
            ));
        }
    }
}
