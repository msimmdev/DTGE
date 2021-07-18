using System.Collections.Generic;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.Interfaces.GameObjects
{
    public interface ITileGraph : IGameSerializable
    {
        IEnumerable<IVertex<IBoardTile>> Vertices { get; }
        IVertex<IBoardTile> FindVertex(IBoardTile tile);
        void AddVertex(IVertex<IBoardTile> vertex);
        void RemoveVertex(IVertex<IBoardTile> vertex);

    }
}
