using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.Gamesystem
{
    public class GenerateBoard : MonoBehaviour
    {
        public void GenerateBoardView(int rows, int columns, int tileRadius, GenerationShapes shape, PositionHelper positionHelper, GameObject hex, Transform parent)
        {

            if (shape == GenerationShapes.Hexagon)
            {
                InitHexShapeBoard(rows, columns, tileRadius, positionHelper, hex, parent);
            }           
            if (shape == GenerationShapes.Triangle)
            {
                InitTriangleShapeBaord(rows, columns, tileRadius, positionHelper, hex, parent);
            }

            if (shape == GenerationShapes.Parrallelogram)
            {
                InitParallShapeBoard(rows, columns, tileRadius, positionHelper, hex, parent);
            }

            if (shape == GenerationShapes.Rectangle)
            {
                InitRectangleShapeBoard(rows, columns, tileRadius, positionHelper, hex, parent);
            }
        }

        private void InitHexShapeBoard(int rows, int columns, int size, PositionHelper positionHelper, GameObject hex, Transform parent)
        {
            Vector3 pos = Vector3.zero;
            int mapSize = Mathf.Max(rows, columns);

            for (int q = -mapSize; q <= mapSize; q++)
            {
                int r1 = Mathf.Max(-mapSize, -q - mapSize);
                int r2 = Mathf.Min(mapSize, -q + mapSize);
                for (int r = r1; r <= r2; r++)
                {
                    Vector3 worldpos = positionHelper.ToWorldPosition(parent, q, r);                  

                    var position = new Vector3(worldpos.x, 0, worldpos.z);

                    GameObject Tile = GameObject.Instantiate(hex, position, hex.transform.rotation, parent.transform);
                    Tile.name = $"AC: [q={q},r={r}]";

                }
            }
        }

        private void InitParallShapeBoard(int rows, int columns, int size, PositionHelper positionHelper, GameObject hex, Transform parent)
        {

            Vector3 pos = Vector3.zero;
            int mapSize = Mathf.Max(rows, columns);

            for (int q = 0; q <= rows; q++)
            {
                for (int r = 0; r <= columns; r++)
                {
                    Vector3 worldpos = positionHelper.ToWorldPosition(parent, q, r);

                    var position = new Vector3(worldpos.x, 0, worldpos.z);

                    GameObject Tile = GameObject.Instantiate(hex, position, hex.transform.rotation, parent.transform);
                    Tile.name = $"AC: [q={q},r={r}]";
                }
            }
        }

        private void InitTriangleShapeBaord(int rows, int columns, int size, PositionHelper positionHelper, GameObject hex, Transform parent)
        {
            Vector3 pos = Vector3.zero;
            int mapSize = Mathf.Max(rows, columns);

            for (int q = 0; q <= mapSize; q++)
            {
                for (int r = 0; r <= mapSize - q; r++)
                {

                    Vector3 worldpos = positionHelper.ToWorldPosition(parent, q, r);

                    var position = new Vector3(worldpos.x, 0, worldpos.z);

                    GameObject Tile = GameObject.Instantiate(hex, position, hex.transform.rotation, parent.transform);
                    Tile.name = $"AC: [q={q},r={r}]";
                }
            }
        }

        private void InitRectangleShapeBoard(int rows, int columns, int size, PositionHelper positionHelper, GameObject hex, Transform parent)
        {

            Vector3 pos = Vector3.zero;
            int mapSize = Mathf.Max(rows, columns);

            for (int q = 0; q <= rows; q++)
            {
                for (int r = 0; r <= columns; r++)
                {
                    Vector3 worldpos = positionHelper.ToWorldPosition(parent, q, r);

                    var position = new Vector3(worldpos.x, 0, worldpos.z);

                    GameObject Tile = GameObject.Instantiate(hex, position, hex.transform.rotation, parent.transform);
                    Tile.name = $"AC: [q={q},r={r}]";
                }
            }
        }

    }

    public enum GenerationShapes
    {
        Hexagon,
        Rectangle,
        Triangle,
        Parrallelogram
    }
}
