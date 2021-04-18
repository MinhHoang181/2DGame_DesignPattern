using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Commands

{
    public class MoveForward : PlayerMove
    {
        public MoveForward(Player player) : base(player)
        {

        }

        public override void Move()
        {
            player.transform.position += Vector3.up * player.Speed * Time.deltaTime * 5f;
        }
    }
}


