using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using Pathfinding;
using UnityEngine;

namespace DesignPattern.State
{
    public class MoveState : IState
    {
        public float TimeStuck { get; private set; }

        private Zombie zombie;
        private Vector3 lastPosition;

        public MoveState(Zombie zombie)
        {
            this.zombie = zombie;
        }

        public void OnEnter()
        {
            TimeStuck = 0f;
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            AIMove();

            if (Vector3.Distance(zombie.transform.position, lastPosition) <= 0f)
            {
                TimeStuck += Time.deltaTime;
            }

            lastPosition = zombie.transform.position;
        }

        private void AIMove()
        {
            if (zombie.PathNodes.Count > 0)
            {
                PathNode currentNode = zombie.PathNodes[0];
                if (currentNode == null) return;
                Vector3 targetPosition = MapController.Instance.Grid.GetWorldPosition(currentNode.X, currentNode.Y);
                if (Vector3.Distance(zombie.transform.position, targetPosition) > 0.5f)
                {
                    Vector3 direction = (targetPosition - zombie.transform.position).normalized;
                    zombie.Move(direction);
                }
                else
                {
                    zombie.PathNodes.RemoveAt(0);
                }
            }
        }
    }
}

