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
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridArray[x, y] = new PathNode(x, y, iswalkable: false);
                }
            }
        }

        public GridPathNode(int width, int height, Vector2 cellSize, Vector3 originPosition) :  base(width, height, cellSize, originPosition)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridArray[x, y] = new PathNode(x, y, iswalkable: false);
                }
            }
        }

        public override void SetGridObject(int x, int y, PathNode value)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                value.Update(x, y);
                gridArray[x, y].Update(value);
                
                TriggerGridObjectChanged(x, y);
                if (debugTextArray[x, y] != default)
                {
                    debugTextArray[x, y].text = gridArray[x, y].ToString();
                }
            }
        }

        public IEnumerable GetNeighbors(int x, int y, DirectionType directionType)
        {
            int dirX, dirY;
            switch (directionType)
            {
                case DirectionType.EIGHT_DIRECTIONS:
                    for (dirX = -1; dirX <= 1; dirX++)
                    {
                        for (dirY = -1; dirY <= 1; dirY++)
                        {
                            if (dirX == 0 && dirY == 0) continue;
                            PathNode neighbor = GetNeighbor(x, y, dirX, dirY);
                            if (neighbor != null)
                            {
                                yield return neighbor;
                            }
                        }
                    }
                    break;
                case DirectionType.FOUR_DIRECTIONS:
                    for (dirX = -1; dirX <= 1; dirX++)
                    {
                        for (dirY = -1; dirY <= 1; dirY++)
                        {
                            if (dirX == 0 && dirY == 0) continue;
                            if (dirX * dirY != 0) continue;
                            PathNode neighbor = GetNeighbor(x, y, dirX, dirY);
                            if (neighbor != null)
                            {
                                yield return neighbor;
                            }
                        }
                    }
                    break;
                default:
                    break;
                    
            }
        }

        private PathNode GetNeighbor(int x, int y, int dirX, int dirY)
        {
            if (x == 0 && y == 0)
            {
                return null;
            }
            int checkX = x + dirX;
            int checkY = y + dirY;
            if (checkX >= 0 && checkX < Width && checkY >= 0 && checkY < Height)
            {
                return gridArray[checkX, checkY];
            }
            return null;
        }
    }
}

