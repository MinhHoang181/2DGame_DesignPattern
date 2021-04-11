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
            switch (player.Facing)
            {
                case Direction.DOWN:
                    Debug.Log("RIGHT");
                    break;
                case Direction.LEFT:
                    Debug.Log("RIGHT");
                    break;
                case Direction.UP:
                    Debug.Log("RIGHT");
                    break;
                case Direction.RIGHT:
                    Debug.Log("Giu Nguyen");
                    break;

            }
        }
    }

}
