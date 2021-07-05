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

        public IGameSerializationData GetSerializationData()
        {
            return new QuadBoardPositionSerializationData()
            {
                X = this.X,
                Y = this.Y
            };
        }

        public void PopulateSerializationData(IGameSerializationData data)
        {
            var objectData = data as QuadBoardPositionSerializationData;
            X = objectData.X;
            Y = objectData.Y;
        }

        public override string ToString()
        {
            return $"{X}:{Y}";
        }
    }
}
