using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Commands

{
    public class MoveRight : PlayerMove
    {
        public MoveRight(Player player) : base(player)
        {

        }

        public override void Move()
        {
            player.transform.Translate(player.transform.right * player.Speed * Time.deltaTime * 0.7f);
        }
    }

}
