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
            player.transform.Translate(player.transform.up * player.Speed * Time.deltaTime * 0.7f);
        }

        public override void SetFacing()
        {
            int angle = 90;
            player.Facing = Direction.UP;
            player.transform.Find("Sprite").transform.eulerAngles = new Vector3(0, 0, angle);
        
        }

        }
}


