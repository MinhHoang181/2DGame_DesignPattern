using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using Pathfinding;
using UnityEngine;

namespace DesignPattern.State
{
    public class SearchState : IState
    {
        private Zombie zombie;

        private Vector3 targetPosition;

        public SearchState(Zombie zombie)
        {
            this.zombie = zombie;
        }

        public void OnEnter()
        {
            targetPosition = zombie.TargetPosition;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            SearchPlayer();
        }

        #region PATHFINDING
        private void SearchPlayer()
        {
            Player player = GameController.Instance.Player;

            if (player == null) return;

            if (targetPosition != player.transform.position)
            {
                targetPosition = player.transform.position;
                zombie.PathNodes = Pathfinding.Pathfinding.Findpath(MapController.Instance.Grid, zombie.transform.position, targetPosition, DirectionType.FOUR_DIRECTIONS);
                //Debug.Log("pathNodes: " + pathNodes.Count);
            }
        }
        #endregion
    }
}

