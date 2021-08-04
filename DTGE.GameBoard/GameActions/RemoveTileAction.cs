using DTGE.Common.Base;
using DTGE.Common.Core;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;

namespace DTGE.GameBoard.GameActions
{
    public class RemoveTileAction : IdentifiedAction, IRemoveTileAction
    {
        public RemoveTileAction(IBoardTile tile)
        {
            Tile = tile;
        }

        public IBoardTile Tile { get; }

        public IGameDto GetDto()
        {
            throw new System.NotImplementedException();
        }

        public void UseDto(IGameDto data, IObjectResolver resolver)
        {
            throw new System.NotImplementedException();
        }

        public ValidationResult Validate()
        {
            if (Tile == null)
                return ValidationResult.NewError("Tile is required for RemoveTile action.");

            if (Tile.Board == null)
                return ValidationResult.NewError("Tile is not attached to board.");

            return ValidationResult.NewSuccess();
        }

        protected override void PerformAction()
        {
            Tile.Board.Tiles.Remove(Tile.Id);
            Tile.Board = null;
            Tile.Position = null;
        }
    }
}
