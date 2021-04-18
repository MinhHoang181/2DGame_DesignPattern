using System;
using UnityEngine;

namespace Pathfinding
{
    public class GridMap<GridObject>
    {
        private int width;
        private int height;
        private Vector2 cellSize;
        private Vector3 originPosition;
        private GridObject[,] gridArray;

        /// <summary>Tao GridMap gom chieu rong, chieu cao.
        /// <para>- Danh cho cac loai object value co ban nhu Bool, Int, Float, ...</para>
        /// <para>- cellSize = (1, 1)</para>
        /// <para>- originPosition = (0, 0, 0)</para>
        /// </summary>
        /// <param name="width">chieu dai Grid</param>
        /// <param name="height">chieu rong Grid</param>
        public GridMap(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.cellSize = Vector2.one;
            this.originPosition = Vector3.zero;

            gridArray = new GridObject[width, height];
        }

        /// <summary>Tao GridMap gom chieu rong, chieu cao, kich thuoc 1 o, vi tri goc.
        /// <para>- Danh cho cac loai object value co ban nhu Bool, Int, Float, ...</para>
        /// </summary>
        /// <param name="width">chieu dai Grid</param>
        /// <param name="height">chieu rong Grid</param>
        /// <param name="cellSize">kich thuoc 1 o Grid</param>
        /// <param name="originPosition">toa do goc</param>
        public GridMap(int width, int height, Vector2 cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new GridObject[width, height];
        }

        /// <summary>Tao GridMap gom chieu rong, chieu cao, kich thuoc 1 o, vi tri goc.
        /// <para>- Danh cho cac loai object tu dinh nghia hoac phuc tap</para>
        /// </summary>
        /// <param name="width">chieu dai Grid</param>
        /// <param name="height">chieu rong Grid</param>
        /// <param name="cellSize">kich thuoc 1 o Grid</param>
        /// <param name="originPosition">toa do goc</param>
        /// <param name="gridObject">kieu object moi, vd: () => new GridObject()</param>
        public GridMap(int width, int height, Vector2 cellSize, Vector3 originPosition, Func<GridObject> gridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new GridObject[width, height];
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(0); y++)
                {
                    gridArray[x, y] = gridObject();
                }
            }
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x * cellSize.x, y * cellSize.y) + originPosition;
        }

        public void SetValue(int x, int y, GridObject value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
            }
        }

        public void SetValue(Vector3 worldPosition, GridObject value)
        {
            GetXY(worldPosition, out int x, out int y);
            SetValue(x, y, value);
        }

        public GridObject GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default;
            }
        }

        public GridObject GetValue(Vector3 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return GetValue(x, y);
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize.x);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize.y);
        }

        public void DebugGrid()
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    GridObject value = GetValue(x, y);
                    if (value.Equals(default(GridObject)))
                    {
                        DrawSquare(x, y, Color.green);
                    }
                    else
                    {
                        DrawSquare(x, y, Color.red);
                    }
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white);
        }

        private void DrawSquare(int x, int y, Color color)
        {
            float offset = 0.02f;
            // Duoi len tren
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color);
            // Trai sang phai
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color);
            // Tren sang phai
            Debug.DrawLine(GetWorldPosition(x, y + 1) - new Vector3(0, offset, 0), GetWorldPosition(x + 1, y + 1) - new Vector3(0, offset, 0), color);
            // Phai len tren
            Debug.DrawLine(GetWorldPosition(x + 1, y) - new Vector3(offset, 0, 0), GetWorldPosition(x + 1, y + 1) - new Vector3(offset, 0, 0), color);
        }
    }

}

