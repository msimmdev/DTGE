using DTGE.Common.Core;
using DTGE.Common.Base;
using DTGE.Common.Interfaces;
using DTGE.GameBoard.Interfaces.GameActions;
using DTGE.GameBoard.Interfaces.GameObjects;
using DTGE.GameBoard.Interfaces.DataTypes;

namespace DTGE.GameBoard.GameActions
{
    public class PlaceTileOnBoardAction : IdentifiedAction, IPlaceTileOnBoardAction
    {
        public PlaceTileOnBoardAction(IBoard board, IBoardTile tile, IBoardPosition position)
        {
            Board = board;
            Tile = tile;
            Position = position;
        }

        public IBoard Board { get; }
        public IBoardTile Tile { get; }
        public IBoardPosition Position { get; }

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
                return ValidationResult.NewError("Tile is required for PlaceTileOnBoard action.");

            if (Board == null)
                return ValidationResult.NewError("Board is required for PlaceTileOnBoard action.");

            if (Position == null)
                return ValidationResult.NewError("Position is required for PlaceTileOnBoard action.");

            if (Tile.Board != null)
                return ValidationResult.NewError("Tile is already attached to a board.");

            if (Tile.Position != null)
                return ValidationResult.NewError("Tile is already has a position.");

            if (Board.HasTile(Position))
                return ValidationResult.NewError("A Tile already exists at this position.");


            return ValidationResult.NewSuccess();
        }

        protected override void PerformAction()
        {
            Tile.Position = Position;
            Board.Tiles.Add(Tile.Id, Tile);
        }
    }
}
