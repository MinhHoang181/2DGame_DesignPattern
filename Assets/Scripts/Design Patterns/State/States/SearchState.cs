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

        public SearchState(Zombie zombie)
        {
            this.zombie = zombie;
        }

        public void OnEnter()
        {
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

            zombie.PathNodes = Pathfinding.Pathfinding.Findpath(MapController.Instance.Grid, zombie.transform.position, player.transform.position, DirectionType.FOUR_DIRECTIONS);
            //Debug.Log("pathNodes: " + pathNodes.Count);
        }
        #endregion
    }
}

