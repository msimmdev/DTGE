using System;
using System.Linq;
using System.Collections.Generic;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.SerializationData;
using DTGE.GameBoard.DataTypes;

namespace DTGE.GameBoard.GameObjects
{
    public class TileGraph : DTGEObject, ITileGraph
    {
        public TileGraph() : base()
        {
            Vertices = new HashSet<IVertex<IBoardTile>>();
        }

        public ISet<IVertex<IBoardTile>> Vertices { get; }
        public Func<Guid, IBoardTile> ObjectResolver { get; set; }

        public IVertex<IBoardTile> FindVertex(IBoardTile tile)
        {
            return Vertices.SingleOrDefault(x => x.Object.Equals(tile));
        }

        public IGameSerializationData GetSerializationData()
        {
            var vertexList = new List<VertexSerializationData>();
            foreach (var vertex in Vertices)
            {
                var edgeList = new List<EdgeSerializationData>();
                foreach (var edge in vertex.Edges)
                {
                    var edgeData = new EdgeSerializationData()
                    {
                        SourceObjectId = edge.Source.Object.Id.ToString(),
                        TargetObjectId = edge.Target.Object.Id.ToString(),
                        Distance = edge.Distance
                    };
                    edgeList.Add(edgeData);
                }
                var vertexData = new VertexSerializationData()
                {
                    ObjectId = vertex.Object.Id.ToString(),
                    Edges = edgeList
                };
                vertexList.Add(vertexData);
            }
            return new TileGraphSerializationData()
            {
                Id = this.Id.ToString(),
                Vertices = vertexList
            };
        }

        public void PopulateSerializationData(IGameSerializationData data)
        {
            var objectData = data as TileGraphSerializationData;
            Id = new Guid(objectData.Id);
            var vertexTracker = new Dictionary<Guid, Vertex<IBoardTile>>();
            foreach (var vertexData in objectData.Vertices)
            {
                var tileObject = ObjectResolver(new Guid(vertexData.ObjectId));
                var vertex = new Vertex<IBoardTile>(tileObject);
                vertexTracker.Add(new Guid(vertexData.ObjectId), vertex);
            }

            foreach (var vertexData in objectData.Vertices)
            {
                var vertex = vertexTracker[new Guid(vertexData.ObjectId)];
                foreach (var edgeData in vertexData.Edges)
                {
                    var edge = new Edge<IBoardTile>(vertexTracker[new Guid(edgeData.SourceObjectId)], vertexTracker[new Guid(edgeData.TargetObjectId)], edgeData.Distance);
                    vertex.AddEdge(edge);
                }
                Vertices.Add(vertex);
            }
        }
    }
}
