using System;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;
using DTGE.GameBoard.DataTypes;

namespace DTGE.GameBoard.GameObjects
{
    public class BoardObject : DTGEObject, IBoardObject
    {
        public IBoard Board { get; set; }
        public IBoardPosition Position { get; set; }

        public IGameSerializationData GetSerializationData()
        {
            return new BoardObjectSerializationData()
            {
                Id = this.Id.ToString(),
                BoardId = this.Board?.Id.ToString() ?? null,
                Position = this.Position?.GetSerializationData() ?? null
            };
        }

        public void PopulateSerializationData(IGameSerializationData data)
        {
            var objectData = data as BoardObjectSerializationData;
            Id = new Guid(objectData.Id);

            if (objectData.Position != null)
            {
                var posType = objectData.Position.GetType();
                if (posType == typeof(QuadBoardPositionSerializationData))
                {
                    var posData = objectData.Position as QuadBoardPositionSerializationData;
                    var newPos = new QuadBoardPosition(posData.X, posData.Y);
                    Position = newPos;
                }
            }
        }
    }
}
