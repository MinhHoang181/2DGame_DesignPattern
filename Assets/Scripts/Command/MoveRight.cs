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

        public override void SetFacing()
        {
            int angle = 0;
            player.Facing = Direction.RIGHT;
            player.transform.Find("Sprite").transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

}
