using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class PathNode
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsWalkable { get; private set; }
        public float Value { get; private set; }

        public int gCost;
        public int hCost;
        public int fCost { get { return gCost + hCost; } }

        public PathNode parent;

        public PathNode(float value)
        {
            Value = value;
        }

        public PathNode(bool isWalkable)
        {
            IsWalkable = isWalkable;
        }

        public PathNode(int x, int y)
        {
            X = x;
            Y = y;
        }

        public PathNode(float value, bool isWalkable)
        {
            Value = value;
            IsWalkable = isWalkable;
        }

        public PathNode(int x, int y, float value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public PathNode(int x, int y, bool iswalkable)
        {
            X = x;
            Y = y;
            IsWalkable = iswalkable;
        }

        public void Update(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(int x, int y, float value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public void Update(int x, int y, bool isWalkable)
        {
            X = x;
            Y = y;
            IsWalkable = isWalkable;
        }

        public override string ToString()
        {
            return $"{Value}\n" +
                   $"{IsWalkable}";
        }

        /// <summary>
        /// Tinh khoan cach hCost giua 2 node A va B tren grid
        /// </summary>
        /// <param name="nodeA">node A</param>
        /// <param name="nodeB">node B</param>
        /// <returns>tra ve khoang cach hCost</returns>
        public static int GetDistance(PathNode nodeA, PathNode nodeB)
        {
            int dstX = System.Math.Abs(nodeA.X - nodeB.X);
            int dstY = System.Math.Abs(nodeA.Y - nodeB.Y);
            return (dstX > dstY) ?
                14 * dstY + 10 * (dstX - dstY) :
                14 * dstX + 10 * (dstY - dstX);
        }

        /// <summary>
        /// Tinh khoan cach hCost giua node hien tai va muc tieu tren grid
        /// </summary>
        /// <param name="target">node muc tieu</param>
        /// <returns>tra ve khoang cach hCost</returns>
        public int GetDistance(PathNode target)
        {
            int dstX = System.Math.Abs(target.X - X);
            int dstY = System.Math.Abs(target.Y - Y);
            return (dstX > dstY) ?
                14 * dstY + 10 * (dstX - dstY) :
                14 * dstX + 10 * (dstY - dstX);
        }
    }
}
