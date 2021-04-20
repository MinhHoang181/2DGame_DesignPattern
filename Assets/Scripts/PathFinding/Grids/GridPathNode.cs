using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Pathfinding
{
    public class GridPathNode : Grid<PathNode>
    {
        public GridPathNode(int width, int height) : base(width, height)
        {
            //for (int x = 0; x < gridArray.GetLength(0); x++)
            //{
            //    for (int y = 0; y < gridArray.GetLength(1); y++)
            //    {
            //        gridArray[x, y] = new PathNode(x, y);
            //    }
            //}
        }

        public GridPathNode(int width, int height, Vector2 cellSize, Vector3 originPosition) :  base(width, height, cellSize, originPosition)
        {
            //for (int x = 0; x < gridArray.GetLength(0); x++)
            //{
            //    for (int y = 0; y < gridArray.GetLength(1); y++)
            //    {
            //        gridArray[x, y] = new PathNode(x, y);
            //    }
            //}
        }

        public override void SetGridObject(int x, int y, PathNode value)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                gridArray[x, y] = value;
                gridArray[x, y].Update(x, y);
                TriggerGridObjectChanged(x, y);
                if (debugTextArray[x, y] != default)
                {
                    debugTextArray[x, y].text = gridArray[x, y].ToString();
                }
            }
        }

        public override void ScanTilemap(Tilemap tilemap, PathNode value)
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] tileArray = tilemap.GetTilesBlock(bounds);
            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = tileArray[x + y * bounds.size.x];
                    if (tile != null)
                    {
                        //Debug.Log("x: " + x + ", y: " + y + " = " + tile.name);
                        SetGridObject(x, y, value);
                    }
                }
            }
        }
    }
}

