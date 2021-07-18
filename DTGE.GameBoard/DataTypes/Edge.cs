using DTGE.GameBoard.Interfaces.DataTypes;
using System.Collections.Generic;

namespace DTGE.GameBoard.DataTypes
{
    public class Edge<T> : IEdge<T>
    {
        public Edge(IVertex<T> source, IVertex<T> target, int distance)
        {
            Source = source;
            Target = target;
            Distance = distance;
        }

        public IVertex<T> Source { get; }
        public IVertex<T> Target { get; }
        public int Distance { get; set; }

        public bool Equals(IEdge<T> other)
        {
            return Source.Equals(other.Source) && Target.Equals(other.Target);
        }

        public override int GetHashCode()
        {
            int hashCode = -1031959520;
            hashCode = hashCode * -1521134295 + EqualityComparer<IVertex<T>>.Default.GetHashCode(Source);
            hashCode = hashCode * -1521134295 + EqualityComparer<IVertex<T>>.Default.GetHashCode(Target);
            return hashCode;
        }
    }
}
