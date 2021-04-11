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
            int angle = 180;
            player.Facing = Direction.LEFT;
            player.transform.Find("Sprite").transform.eulerAngles = new Vector3(0, 0, angle);
                
        }

    }  
}
