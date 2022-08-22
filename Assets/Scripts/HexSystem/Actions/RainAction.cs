using DAE.BoardSystem;
using DAE.HexSystem;
using DAE.HexSystem.Actions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace DAE.HexSystem.Actions
{

    class RainAction<TCard, TPiece> : ActionBase<TCard, TPiece> where TPiece : IPiece where TCard : ICard
    {


        public override void ExecuteAction(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
        {

            ActionHelper<TCard, TPiece> actionHelper = new ActionHelper<TCard, TPiece>(board, grid, position, piece, card);
            actionHelper.Direction0(10)
                        .Direction1(10)
                        .Direction2(10)
                        .Direction3(10)
                        .Direction4(10)
                        .Direction5(10);

            actionHelper.Collect().Shuffle();

            actionHelper.Collect().RemoveRange(3, actionHelper.Collect().Count - 3);
            //board.Move(piece, position);

            var destroylist = actionHelper.Collect();

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
            List<IHex> Valid = new List<IHex>();

            Valid.Add(position);

            return Valid;
        }

        public override List<IHex> Validpositions(Board<IHex, TPiece> board, Grid<IHex> grid, IHex position, TPiece piece, CardType card)
        {

            List<IHex> Valid = new List<IHex>();

            Valid.Add(position);

            return Valid;

        }
    }
}



public static class MyExtensions
{
    private static readonly System.Random rng = new System.Random();

    //Fisher - Yates shuffle
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

