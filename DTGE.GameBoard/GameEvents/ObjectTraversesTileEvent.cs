using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.GameEvents
{
    public class ObjectMovesToTileEvent : IGameEvent
    {
        public ObjectMovesToTileEvent(IBoardObject boardObject, IBoardTile boardTile)
        {
            Object = boardObject;
            Tile = boardTile;
        }

        public IBoardObject Object { get; }
        public IBoardTile Tile { get; }
    }
}
