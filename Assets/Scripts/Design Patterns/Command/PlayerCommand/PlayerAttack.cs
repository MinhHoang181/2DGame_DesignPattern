using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Commands
{
    public class PlayerAttack : PlayerCommand
    {
        public PlayerAttack() { }
        public PlayerAttack(Player player)
        {
            this.player = player;
        }

        public override void Execute()
        {
            Attack();
        }

        private void Attack()
        {
            player.Attack();
        }
    }
}
