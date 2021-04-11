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
            player.transform.Translate(player.transform.right * -player.Speed  *Time.deltaTime * 0.7f);


        }
        public override void SetFacing()
        {
            int angle = 0;
            Direction facing = player.Facing;
            player.Facing = Direction.LEFT;
            switch(facing)
            {

                case Direction.DOWN:
                    angle = 180;
                    break;
                case Direction.LEFT:
                    angle = 180;
                    break;
                case Direction.UP:
                    angle = 180;
                    break;
                case Direction.RIGHT:
                    angle = 180;
                    break;
            }
            player.transform.FindChild("Sprite").Rotate(new Vector3(0, 0, angle));
                
        }

    }  
}
