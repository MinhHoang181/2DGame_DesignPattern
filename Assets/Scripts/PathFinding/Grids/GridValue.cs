using UnityEngine;

namespace Pathfinding
{
    public class GridValue<T> : Grid<T> where T: unmanaged
    {
        public GridValue(int width, int height) : base(width, height) { }
        public GridValue(int width, int height, Vector2 cellSize, Vector3 originPosition) : base(width, height, cellSize, originPosition) { }
    }
}

