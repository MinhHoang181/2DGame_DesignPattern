using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Commands
{
    public class MoveBack : PlayerMove
    {
        public MoveBack(Player player) : base(player)
        {

        }
        public override void Move()
        {
            player.transform.Translate(player.transform.up * -player.Speed  *Time.deltaTime);
        }
    }
}

