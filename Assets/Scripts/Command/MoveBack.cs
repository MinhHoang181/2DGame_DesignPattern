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
            player.transform.Translate(player.transform.up * -player.Speed  *Time.deltaTime * 0.7f);
        }

        public override void SetFacing()
        {
            switch (player.Facing)
            {
                case Direction.DOWN:
                    Debug.Log("GIU NGUYEN");
                    break;
                case Direction.LEFT:
                    Debug.Log("DOWN");
                    break;
                case Direction.UP:
                    Debug.Log("DOWN");
                    break;
                case Direction.RIGHT:
                    Debug.Log("DOWN");
                    break;

            }
        }
    }
}

