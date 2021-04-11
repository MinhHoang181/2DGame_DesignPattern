using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;
    
    namespace DesignPattern.Commands
{

    public class PlayerChangeWeapon : Command
    {
        private protected Player player;

        public PlayerChangeWeapon(Player player)
        {
            this.player = player;
        }

        public override void Execute()
        {
            ChangeWeapon();
        }

        public void ChangeWeapon()
        {
            player.ChangeWeapon();

        }


    }
}
