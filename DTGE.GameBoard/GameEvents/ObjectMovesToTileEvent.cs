using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.GameEvents
{
    public class ObjectTraversesTileEvent : IGameEvent
    {
        public ObjectTraversesTileEvent(IBoardObject boardObject, IBoardTile boardTile)
        {
            Object = boardObject;
            Tile = boardTile;
        }

        public IBoardObject Object { get; }
        public IBoardTile Tile { get; }
    }
}
