using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Pathfinding
{
    public enum DirectionType
    {
        FOUR_DIRECTIONS,
        EIGHT_DIRECTIONS,
    }

    public class Grid<GridObject>
    {
        /// <summary>
        /// Tra ve chieu dai cua Grid
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// Tra ve chieu cao cua Grid
        /// </summary>
        public int Height { get; protected set; }
        /// <summary>
        /// Tra ve kich thuoc cua mot o trong grid
        /// </summary>
        public Vector2 CellSize { get; protected set; }
        /// <summary>
        /// Event bat su kien thay doi gia tri trong grid
        /// </summary>
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs: EventArgs
        {
            public int x;
            public int y;
        }

        protected Vector3 originPosition;
        protected GridObject[,] gridArray;
        protected Text[,] debugTextArray;

        /// <summary>Tao GridMap gom chieu rong, chieu cao.
        /// <para>- Danh cho cac loai object value co ban nhu Bool, Int, Float, ...</para>
        /// <para>- cellSize = (1, 1)</para>
        /// <para>- originPosition = (0, 0, 0)</para>
        /// </summary>
        /// <param name="width">chieu dai Grid</param>
        /// <param name="height">chieu rong Grid</param>
        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            CellSize = Vector2.one;
            originPosition = Vector3.zero;

            gridArray = new GridObject[Width, Height];
            debugTextArray = new Text[Width, Height];
        }

        /// <summary>Tao GridMap gom chieu rong, chieu cao, kich thuoc 1 o, vi tri goc.
        /// <para>- Danh cho cac loai object value co ban nhu Bool, Int, Float, ...</para>
        /// </summary>
        /// <param name="width">chieu dai Grid</param>
        /// <param name="height">chieu rong Grid</param>
        /// <param name="cellSize">kich thuoc 1 o Grid</param>
        /// <param name="originPosition">toa do goc</param>
        public Grid(int width, int height, Vector2 cellSize, Vector3 originPosition)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new GridObject[Width, Height];
            debugTextArray = new Text[Width, Height];
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x * CellSize.x, y * CellSize.y);
        }

        /// <summary>
        /// Dat gia tri tai vi tri [x, y] trong grid
        /// </summary>
        /// <param name="x">toa do x trong grid</param>
        /// <param name="y">toa do y trong grid</param>
        /// <param name="value">gia tri tai vi tri [x, y]</param>
        public virtual void SetGridObject(int x, int y, GridObject value)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                gridArray[x, y] = value;
                TriggerGridObjectChanged(x, y);
                if (debugTextArray[x, y] != default)
                {
                    debugTextArray[x, y].text = gridArray[x, y].ToString();
                }
            }
        }

        /// <summary>
        /// Dat gia tri tai vi tri [x, y] trong grid
        /// </summary>
        /// <param name="x">toa do x trong grid</param>
        /// <param name="y">toa do y trong grid</param>
        /// <param name="value">gia tri tai vi tri [x, y]</param>
        public virtual void SetGridObject<T>(int x, int y, T value) where T: unmanaged
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                GridObject gridObject = (GridObject)(object)value;
                gridArray[x, y] = gridObject;
                TriggerGridObjectChanged(x, y);
                if (debugTextArray[x, y] != default)
                {
                    debugTextArray[x, y].text = gridArray[x, y].ToString();
                }
            }
        }

        /// <summary>
        /// Dat gia tri tai vi tri thuc tuong duong voi vi tri grid
        /// </summary>
        /// <param name="worldPosition">vi tri thuc</param>
        /// <param name="value">gia tri cua vi tri thuc tuong duong vi tri [x, y] trong grid</param>
        public void SetGridObject(Vector3 worldPosition, GridObject value)
        {
            GetXY(worldPosition, out int x, out int y);
            SetGridObject(x, y, value);
        }

        /// <summary>
        /// Thong bao su kien thay doi gia tri tai vi tri [x, y] trong grid
        /// </summary>
        /// <param name="x">toa do x</param>
        /// <param name="y">toa do y</param>
        public void TriggerGridObjectChanged(int x, int y)
        {
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }

        /// <summary>
        /// Lay object duoc luu trong grid tai vi tri [x, y]
        /// </summary>
        /// <param name="x">toa do x</param>
        /// <param name="y">toa do y</param>
        /// <returns>tra ve kieu object tuong ung</returns>
        public GridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Lay object duoc luu trong grid tai vi tri thuc tuong ung
        /// </summary>
        /// <param name="worldPosition">toa do thuc</param>
        /// <returns>tra ve kieu object tuong ung</returns>
        public GridObject GetGridObject(Vector3 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return GetGridObject(x, y);
        }

        /// <summary>
        /// Lay toa doa x, y tren gridmap tu toa do tren game
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <param name="x">toa do x tren grid</param>
        /// <param name="y">toa do y tren grid</param>
        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / CellSize.x);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / CellSize.y);
        }

        /// <summary>
        /// Quet tilemap de cap nhat gia tri gridmap
        /// </summary>
        /// <param name="tilemap">tilemap dung de quet</param>
        /// <param name="value">gia tri cua tilebase tren gridmap</param>
        public void ScanTilemap(Tilemap tilemap, GridObject value)
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

        /// <summary>
        /// ve grid debug len scene
        /// </summary>
        public void DebugGrid(int sizeText = 20)
        {
            float duration = Mathf.Infinity;
            GameObject canvas = new GameObject("Debug", typeof(Canvas));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            canvas.GetComponent<Canvas>().sortingLayerName = SortingLayer.layers[SortingLayer.layers.Length - 1].name;
            canvas.GetComponent<Canvas>().sortingOrder = 100;

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    GridObject value = GetGridObject(x, y);
                    if (value == null)
                    {
                        DrawSquare(x, y, Color.red, duration);
                        continue;
                    } else if (value.Equals(default))
                    {
                        DrawSquare(x, y, Color.red, duration);
                    } else
                    {
                        DrawSquare(x, y, Color.white, duration);
                    }
                    debugTextArray[x, y] = CreateWorldText(x, y, canvas.transform, gridArray[x, y].ToString(), sizeText, Color.white);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, Height) + originPosition, GetWorldPosition(Width, Height) + originPosition, Color.white, duration);
            Debug.DrawLine(GetWorldPosition(Width, 0) + originPosition, GetWorldPosition(Width, Height) + originPosition, Color.white, duration);
        }

        protected void DrawSquare(int x, int y, Color color, float duration)
        {
            float offset = 0.02f;
            // Duoi len tren
            Debug.DrawLine(GetWorldPosition(x, y) + originPosition, GetWorldPosition(x, y + 1) + originPosition, color, duration);
            // Trai sang phai
            Debug.DrawLine(GetWorldPosition(x, y) + originPosition, GetWorldPosition(x + 1, y) + originPosition, color, duration);
            // Tren sang phai
            Debug.DrawLine(GetWorldPosition(x, y + 1) - new Vector3(0, offset, 0) + originPosition, GetWorldPosition(x + 1, y + 1) - new Vector3(0, offset, 0) + originPosition, color, duration);
            // Phai len tren
            Debug.DrawLine(GetWorldPosition(x + 1, y) - new Vector3(offset, 0, 0) + originPosition, GetWorldPosition(x + 1, y + 1) - new Vector3(offset, 0, 0) + originPosition, color, duration);
        }

        protected Text CreateWorldText(int x, int y, Transform parent, string value, int sizeText, Color color)
        {
            GameObject gameObject = new GameObject("x" + x + "y" + y);
            Transform transform = gameObject.transform;
            
            transform.SetParent(parent, false);
            transform.localPosition = GetWorldPosition(x, y) + originPosition + new Vector3(CellSize.x, CellSize.y) * .5f;
            transform.localScale = new Vector3(0.01f, 0.01f);

            Text text = gameObject.AddComponent<Text>();
            text.text = value;
            text.alignment = TextAnchor.MiddleCenter;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = sizeText;
            text.color = color;
            
            return text;
        }
    }
}

