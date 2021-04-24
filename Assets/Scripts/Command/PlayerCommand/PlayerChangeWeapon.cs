using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;
    
    namespace DesignPattern.Commands
{

    public class PlayerChangeWeapon : PlayerCommand
    {
        public PlayerChangeWeapon() { }
        public PlayerChangeWeapon(Player player)
        {
            this.player = player;
        }

        public override void Execute()
        {
            ChangeWeapon();
        }

        private void ChangeWeapon()
        {
            player.ChangeWeapon();
        }
    }
}
