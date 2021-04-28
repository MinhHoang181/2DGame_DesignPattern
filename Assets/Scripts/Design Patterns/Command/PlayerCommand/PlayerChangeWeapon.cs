using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Commands
{

    public abstract class PlayerChangeWeapon : PlayerCommand
    {
        public PlayerChangeWeapon() { }
        public PlayerChangeWeapon(Player player)
        {
            this.player = player;
        }
    }

    public class PlayerChangeNextWeapon : PlayerChangeWeapon
    {
        public PlayerChangeNextWeapon() : base() { }
        public PlayerChangeNextWeapon(Player player) : base(player) { }

        public override void Execute()
        {
            player.Weapon.ChangeNextWeapon();
        }
    }

    public class PlayerChangePrevioustWeapon : PlayerChangeWeapon
    {
        public PlayerChangePrevioustWeapon() : base() { }
        public PlayerChangePrevioustWeapon(Player player) : base(player) { }

        public override void Execute()
        {
            player.Weapon.ChangePrevWeapon();
        }
    }
}
