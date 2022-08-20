using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAE.BoardSystem;
using DAE.HexSystem;

namespace DAE.Gamesystem
{
    [CreateAssetMenu(menuName = "DAE/Position Helper")]

    public class PositionHelper : ScriptableObject
    {
  
        public float TileRadius;

        public Vector2 ToGridPosition(Transform parent, Vector3 worldPosition)
        {

            var q = ((2f / 3f) * worldPosition.x) / TileRadius;
            var r = ((-1f / 3f) * worldPosition.x) + (Mathf.Sqrt(3f) / 3f * worldPosition.z) / TileRadius;

            var x = (int)Mathf.Round(q);
            var y = (int)Mathf.Round(r);

            return new Vector2(x, y);
        }

        public Vector3 ToWorldPosition(Transform parent, int x, int y)
        {

            var q = TileRadius * ((3f / 2f) * x);
            var r = TileRadius * (Mathf.Sqrt(3f) / 2f * x + Mathf.Sqrt(3f) * y);

            var tileposition = new Vector3(q, 0, r);



            return tileposition;
        }

    }

}

