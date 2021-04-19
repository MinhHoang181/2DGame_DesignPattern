using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Pathfinding
{
    public class GridMap<GridObject>
    {
        /// <summary>
        /// Tra ve chieu dai cua Grid
        /// </summary>
        public int Width { get { return width; } }
        /// <summary>
        /// Tra ve chieu cao cua Grid
        /// </summary>
        public int Height { get { return height; } }
        /// <summary>
        /// Tra ve kich thuoc cua mot o trong grid
        /// </summary>
        public Vector2 CellSize { get { return cellSize; } }
        /// <summary>
        /// Event bat su kien thay doi gia tri trong grid
        /// </summary>
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs: EventArgs
        {
            public int x;
            public int y;
        }

        private int width;
        private int height;
        private Vector2 cellSize;
        private Vector3 originPosition;
        private GridObject[,] gridArray;
        private Text[,] debugTextArray;

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
            debugTextArray = new Text[width, height];
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
            debugTextArray = new Text[width, height];
        }

        /// <summary>Tao GridMap gom chieu rong, chieu cao, kich thuoc 1 o, vi tri goc.
        /// <para>- Danh cho cac loai object tu dinh nghia hoac phuc tap</para>
        /// </summary>
        /// <param name="width">chieu dai Grid</param>
        /// <param name="height">chieu rong Grid</param>
        /// <param name="cellSize">kich thuoc 1 o Grid</param>
        /// <param name="originPosition">toa do goc</param>
        /// <param name="gridObject">kieu object moi, vd: (GridMap<GridObject> g, int x, int y) => new GridObject(g, x, y)</param>
        public GridMap(int width, int height, Vector2 cellSize, Vector3 originPosition, Func<GridMap<GridObject>, int, int, GridObject> gridObject)
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
                    gridArray[x, y] = gridObject(this, x, y);
                }
            }
            debugTextArray = new Text[width, height];
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x * cellSize.x, y * cellSize.y) + originPosition;
        }

        /// <summary>
        /// Dat gia tri tai vi tri [x, y] trong grid
        /// </summary>
        /// <param name="x">toa do x trong grid</param>
        /// <param name="y">toa do y trong grid</param>
        /// <param name="value">gia tri tai vi tri [x, y]</param>
        public void SetGridObject(int x, int y, GridObject value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
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
            if (x >= 0 && y >= 0 && x < width && y < height)
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
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize.x);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize.y);
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
        public void DebugGrid()
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
                    if (value.Equals(default(GridObject)))
                    {
                        DrawSquare(x, y, Color.red, duration);
                    }
                    else
                    {
                        DrawSquare(x, y, Color.white, duration);
                    }
                    debugTextArray[x, y] = CreateWorldText(x, y, canvas.transform, gridArray[x, y].ToString(), Color.white);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, duration);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, duration);
        }

        private void DrawSquare(int x, int y, Color color, float duration)
        {
            float offset = 0.02f;
            // Duoi len tren
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), color, duration);
            // Trai sang phai
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), color, duration);
            // Tren sang phai
            Debug.DrawLine(GetWorldPosition(x, y + 1) - new Vector3(0, offset, 0), GetWorldPosition(x + 1, y + 1) - new Vector3(0, offset, 0), color, duration);
            // Phai len tren
            Debug.DrawLine(GetWorldPosition(x + 1, y) - new Vector3(offset, 0, 0), GetWorldPosition(x + 1, y + 1) - new Vector3(offset, 0, 0), color, duration);
        }

        private Text CreateWorldText(int x, int y, Transform parent, string value, Color color)
        {
            GameObject gameObject = new GameObject("x" + x + "y" + y);
            Transform transform = gameObject.transform;
            
            transform.SetParent(parent, false);
            transform.localPosition = GetWorldPosition(x, y) + new Vector3(cellSize.x, cellSize.y) * .5f;
            transform.localScale = new Vector3(0.01f, 0.01f);

            Text text = gameObject.AddComponent<Text>();
            text.text = value;
            text.alignment = TextAnchor.MiddleCenter;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 30;
            text.color = color;
            
            return text;
        }
    }

}

