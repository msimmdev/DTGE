using System;
using DTGE.Common.Base;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.SerializationData;
using DTGE.GameBoard.DataTypes;

namespace DTGE.GameBoard.GameObjects
{
    public class BoardTile : IdentifiedObject, IBoardTile
    {
        public IBoard Board { get; set; }
        public IBoardPosition Position { get; set; }

        public IGameDto GetDto()
        {
            return new BoardTileDto()
            {
                Id = this.Id.ToString(),
                BoardId = this.Board?.Id.ToString(),
                Position = this.Position?.GetDto() as BoardPositionDto
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as BoardTileDto;
            Id = new Guid(objectData.Id);

            if (objectData.Position != null)
            {
                var posData = objectData.Position;
                var newPos = resolver.Create<IBoardPosition>(posData);
                Position = newPos;
            }
        }
    }
}
