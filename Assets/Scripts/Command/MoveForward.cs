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
            switch (player.Facing)
            {
                case Direction.DOWN:
                    Debug.Log("UP");
                    break;
                case Direction.LEFT:
                    Debug.Log("UP");
                    break;
                case Direction.UP:
                    Debug.Log("Giu Nguyen");
                    break;
                case Direction.RIGHT:
                    Debug.Log("UP");
                    break;

            }
        }

        }
}


