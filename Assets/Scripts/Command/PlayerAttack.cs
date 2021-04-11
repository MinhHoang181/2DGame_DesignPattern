using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Commands
{
    public class PlayerAttack : Command
    {
        private protected Player player;

        public PlayerAttack(Player player)
        {
            this.player = player;
        }

        public override void Execute()
        {
            Attack();
        }

        public void Attack()
        {
            player.Attack();

        }
    }
}
