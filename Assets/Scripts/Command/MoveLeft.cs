using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Commands

{
    public class MoveLeft : PlayerMove
    {
        public MoveLeft(Player player) : base(player)
        {

        }

        public override void Move()
        {
            player.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            player.transform.Translate(player.transform.right * -player.Speed  *Time.deltaTime * 0.7f);
        }
    }
}
