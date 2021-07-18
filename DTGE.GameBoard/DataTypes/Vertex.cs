using System;
using System.Linq;
using System.Collections.Generic;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.DataTypes
{
    public class Vertex<T> : IVertex<T>
    {

        public Vertex(T attach)
        {
            edges = new List<IEdge<T>>();
            Object = attach;
        }

        public T Object { get; set; }

        private List<IEdge<T>> edges;
        public IEnumerable<IEdge<T>> Edges { get { return edges; } }

        public void AddEdge(IEdge<T> edge)
        {
            edges.Add(edge);
        }

        public bool Equals(IVertex<T> other)
        {
            return Object.Equals(other.Object);
        }

        public bool IsAdjacent(IVertex<T> other)
        {
            return edges.Any(x => x.Target.Equals(other));
        }

        public IEnumerable<IVertex<T>> Neighbors()
        {
            return edges.Select(x => x.Target);
        }

        public void RemoveEdge(IEdge<T> edge)
        {
            edges.Remove(edge);
        }

        public override int GetHashCode()
        {
            return 162302186 + EqualityComparer<T>.Default.GetHashCode(Object);
        }
    }
}
