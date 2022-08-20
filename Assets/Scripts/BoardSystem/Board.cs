using DAE.Commons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DAE.BoardSystem
{
    public class PlacedEventArgs<THex, TPiece> : EventArgs
    {
        public THex ToPosition { get; }

        public TPiece Piece { get; }

        public PlacedEventArgs(THex toPosition, TPiece piece)
        {
            ToPosition = toPosition;
            Piece = piece;
        }
    }

    public class MovedEventArgs<THex, TPiece> : EventArgs
    {
        public THex ToPosition { get; }
        public THex FromPosition { get; }
        public TPiece Piece { get; }

        public MovedEventArgs(THex toPosition, THex fromPosition, TPiece piece)
        {
            ToPosition = toPosition;
            FromPosition = fromPosition;
            Piece = piece;
        }
    }

    public class TakenEventArgs<THex, TPiece> : EventArgs
    {
        public THex FromPosition { get; }
        public TPiece Piece { get; }

        public TakenEventArgs(THex fromPosition, TPiece piece)
        {
            FromPosition = fromPosition;
            Piece = piece;
        }
    }


    public class Board<THex, TPiece>
    {

        private BidirectionalDictionary<THex, TPiece> _positionPiece = new BidirectionalDictionary<THex, TPiece>();


        public bool Place(TPiece piece, THex toPosition)
        {
            if (TryGetPieceAt(toPosition, out _))
                return false;

            if (TryGetPositionOf(piece, out _))
                return false;

            _positionPiece.Add(toPosition, piece);
            OnPlaced(new PlacedEventArgs<THex, TPiece>(toPosition, piece));

            return true;

        }

        public bool Move(TPiece piece, THex toPosition)
        {
            if (TryGetPieceAt(toPosition, out _))
                return false;

            if (!TryGetPositionOf(piece, out var fromPosition))
                return false;

            if (!_positionPiece.Remove(piece))
                return false;

            _positionPiece.Add(toPosition, piece);

            OnMoved(new MovedEventArgs<THex, TPiece>(toPosition, fromPosition, piece));

            return true;
        }

        public bool Take(TPiece piece)
        {
            if (!TryGetPositionOf(piece, out var fromPosition))
                return false;

            if (!_positionPiece.Remove(piece))
                return false;

            OnTaken(new TakenEventArgs<THex, TPiece>(fromPosition, piece));
            return true;
        }


        public bool TryGetPieceAt(THex position, out TPiece piece) => _positionPiece.TryGetValue(position, out piece);

        public bool TryGetPositionOf(TPiece piece, out THex position) => _positionPiece.TryGetKey(piece, out position);


        #region events

        public event EventHandler<PlacedEventArgs<THex, TPiece>> placed;
        public event EventHandler<MovedEventArgs<THex, TPiece>> moved;
        public event EventHandler<TakenEventArgs<THex, TPiece>> taken;

        protected virtual void OnPlaced(PlacedEventArgs<THex, TPiece> eventargs)
        {
            var handlers = placed;
            handlers?.Invoke(this, eventargs);
        }
        protected virtual void OnMoved(MovedEventArgs<THex, TPiece> eventargs)
        {
            var handlers = moved;
            handlers?.Invoke(this, eventargs);
        }
        protected virtual void OnTaken(TakenEventArgs<THex, TPiece> eventargs)
        {
            var handlers = taken;
            handlers?.Invoke(this, eventargs);
        }


        #endregion
    }
}
