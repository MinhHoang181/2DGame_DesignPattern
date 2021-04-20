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
            player.transform.position += Vector3.right * -player.Speed  *Time.deltaTime * 5f;
        }
    }  
}
