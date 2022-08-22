using DAE.BoardSystem;
using DAE.HexSystem;
using DAE.HexSystem.Actions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.HexSystem.Actions
{

    class BombAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {


        public override void ExecuteAction(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
        {
            //board.Move(piece, position);

            var destroylist = IsolatedPositions(board, grid, position, piece, card);

            grid.Destroy(destroylist);

            board.TryGetPositionOf(piece, out var player);
            if (destroylist.Contains(player))
            {
                board.Take(piece);
            }

            foreach (var hex in destroylist)
            {
                if (board.TryGetPieceAt(hex, out var enemy))
                {
                    board.Take(enemy);
                }
            }

        }

        public override List<IHex> IsolatedPositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
        {

                     
            var Valid = ActionHelper<TCard, TPiece>.Neighbours(position, board, grid);
            Valid.Add(position);

            return Valid;
        }

        public override List<IHex> Validpositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
        {

            var Valid = ActionHelper<TCard, TPiece>.Neighbours(position, board, grid);
            Valid.Add(position);

            return Valid;

        }
    }
}