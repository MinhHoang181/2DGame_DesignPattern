using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinding
    {
        public static List<PathNode> Findpath(GridPathNode grid, Vector2 startPos, Vector2 targetPos, DirectionType directionType, bool ignoreValue = false)
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
            //Debug.Log("startNode: " + startNode.ToString());
            PathNode targetNode = grid.GetGridObject(targetPos);
            //Debug.Log("targetNode: " + targetNode.ToString());
            List<PathNode> openSet = new List<PathNode>();
            HashSet<PathNode> closedSet = new HashSet<PathNode>();
            openSet.Add(startNode);
            //Debug.Log("openSet[0]: " + openSet[0].ToString());
            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet[0];
                //Debug.Log("currentNode: " + currentNode.ToString());
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
                    //Debug.Log("target: " + targetNode.ToString());
                    return RetracePath(startNode, targetNode);
                }
                
                foreach (PathNode neighbor in grid.GetNeighbors(currentNode.X, currentNode.Y, directionType))
                {
                    //Debug.Log("Node: " + currentNode.ToString() + ": " + neighbor.ToString());
                    if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    int newMovementCostToNeightbor = currentNode.gCost + currentNode.GetDistance(neighbor) * (ignoreValue ? 1 : (int)(10f * neighbor.Value));
                    //Debug.Log("newcostToNeighbor: " + neighbor.ToString() + "\n cost = " + newMovementCostToNeightbor); 
                    if (newMovementCostToNeightbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeightbor;
                        neighbor.hCost = neighbor.GetDistance(targetNode);
                        neighbor.parent = currentNode;
                        //Debug.Log("Node: " + neighbor.ToString() + "\n parent: " + neighbor.parent.ToString());
                        if (!openSet.Contains(neighbor))
                        {
                            //Debug.Log("Add to openSet: " + neighbor.ToString());
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
                //Debug.Log("Node: " + currentNode.ToString());
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }
    }
}

