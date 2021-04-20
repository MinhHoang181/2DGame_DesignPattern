using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinding
    {
        public static List<PathNode> Findpath(GridPathNode grid, Vector2 startPos, Vector2 targetPos, DirectionType directionType = DirectionType.EIGHT_DIRECTIONS, bool ignoreValue = false)
        {
            List<PathNode> nodesPath = ImpFindPath(grid, startPos, targetPos, directionType, ignoreValue);
            List<PathNode> path = new List<PathNode>();
            if (nodesPath != null)
            {
                foreach (PathNode node in nodesPath)
                {
                    path.Add(node);
                }
            }
            return path;
        }

        private static List<PathNode> ImpFindPath(GridPathNode grid, Vector2 startPos, Vector2 targetPos, DirectionType directionType, bool ignoreValue)
        {
            PathNode startNode = grid.GetGridObject(startPos);
            PathNode targetNode = grid.GetGridObject(targetPos);
            List<PathNode> openSet = new List<PathNode>();
            HashSet<PathNode> closedSet = new HashSet<PathNode>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost ||
                        openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                if (currentNode == targetNode)
                {
                    return RetracePath(startNode, targetNode);
                }

                foreach (PathNode neighbor in grid.GetNeighbors(currentNode.X, currentNode.Y, directionType))
                {
                    if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    int newMovementCostToNeightbor = currentNode.gCost + currentNode.GetDistance(neighbor) * (ignoreValue ? 1 : (int)(10f * neighbor.Value));

                    if (newMovementCostToNeightbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeightbor;
                        neighbor.hCost = neighbor.GetDistance(targetNode);
                        neighbor.parent = currentNode;
                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }

        private static List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();
            PathNode currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }
    }
}

