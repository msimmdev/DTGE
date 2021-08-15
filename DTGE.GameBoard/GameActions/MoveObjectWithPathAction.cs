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
    public class MoveObjectWithPathAction : IdentifiedAction, IMoveObjectWithPathAction
    {
        public MoveObjectWithPathAction(IBoardObject boardObject, ITilePath path)
        {
            Id = Guid.NewGuid();
            Object = boardObject;
            Path = path;
        }

        public IBoardObject Object { get; private set; }

        public ITilePath Path { get; private set; }

        public IGameDto GetDto()
        {
            return new MoveObjectWithPathActionDto()
            {
                Id = Id.ToString(),
                ObjectId = Object.Id.ToString(),
                Path = Path.GetDto() as TilePathDto,
                Tags = Tags.ToList()
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as MoveObjectWithPathActionDto;
            Id = new Guid(objectData.Id);
            Object = resolver.Resolve<IBoardObject>(new Guid(objectData.ObjectId));
            Path = resolver.Create<ITilePath>(objectData.Path);
            Tags = new HashSet<string>(objectData.Tags);
        }

        public ValidationResult Validate()
        {
            if (Object.Board == null)
                return ValidationResult.NewError("Object is not attached to a board.");

            if (Object.Position == null)
                return ValidationResult.NewError("Object must have a starting position.");

            if (Path.Tiles == null || Path.Tiles.Count() < 2)
                return ValidationResult.NewError("Path must have at least 2 tiles.");

            if (!Object.Position.Equals(Path.Tiles.First().Position) || !Object.Board.Equals(Path.Tiles.First().Board))
                return ValidationResult.NewError("Object must have same position and board as first path tile.");

            return ValidationResult.NewSuccess();
        }

        public void Execute(IEventHandler handler)
        {
            handler.Dispatch<ActionStartEvent<MoveObjectWithPathAction>>(
                new ActionStartEvent<MoveObjectWithPathAction>(this));

            PerformAction(handler);

            handler.Dispatch<ActionEndEvent<MoveObjectWithPathAction>>(
                new ActionEndEvent<MoveObjectWithPathAction>(this));
        }

        private void PerformAction(IEventHandler handler)
        {
            foreach (var tile in Path.Tiles)
            {
                Object.Board = tile.Board;
                Object.Position = tile.Position;
                if (tile.Equals(Path.Tiles.Last())) {
                    handler.Dispatch(new ObjectMovesToTileEvent(
                        Object,
                        tile
                    ));
                } else {
                    handler.Dispatch(new ObjectTraversesTileEvent(
                        Object,
                        tile
                    ));
                }
            }
        }
    }
}
