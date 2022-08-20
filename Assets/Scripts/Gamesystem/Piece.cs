using DAE.HexSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.Gamesystem
{
    public class Piece : MonoBehaviour, IPiece
    {
        [SerializeField]
        private pieceType _pieceType;
        public pieceType PieceType => _pieceType;

        public void MoveTo(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        internal void Place(Vector3 worldPosition)
        {
            transform.position = worldPosition;
            gameObject.SetActive(true);
        }

        internal void Taken()
        {
            gameObject.SetActive(false);
        }

    }
}
