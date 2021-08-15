using System;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.SerializationData;

namespace DTGE.GameBoard.DataTypes
{
    /// <summary>
    /// A representation of a position in a 2 dimensional (rectangular) grid.
    /// </summary>
    public class QuadBoardPosition : IBoardPosition, IEquatable<QuadBoardPosition>, IGameSerializable
    {
        public QuadBoardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public QuadBoardPosition(BoardPositionDto data)
        {
            UseDto(data, null);
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object other)
        {
            return Equals(other as QuadBoardPosition);
        }

        public bool Equals(IBoardPosition other)
        {
            return Equals(other as QuadBoardPosition);
        }

        public bool Equals(QuadBoardPosition other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public IGameDto GetDto()
        {
            return new BoardPositionDto()
            {
                Type = this.GetType().AssemblyQualifiedName,
                Position = $"{X}x{Y}"
            };
        }

        public void UseDto(IGameDto data, IResolver resolver)
        {
            var objectData = data as BoardPositionDto;
            var posList = objectData.Position.Split('x');
            X = int.Parse(posList[0]);
            Y = int.Parse(posList[1]);
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }
    }
}
