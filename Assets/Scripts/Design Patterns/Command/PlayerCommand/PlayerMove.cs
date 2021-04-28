using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Commands
{
    public abstract class PlayerMove : PlayerCommand
    {
        public PlayerMove() { }

        public PlayerMove(Player player)
        {
            this.player = player;
        }

        public void Move(Vector3 direction)
        {
            player.Move(direction);
        }
    }

    public class MoveUp : PlayerMove
    {
        public MoveUp() : base() { }
        public MoveUp(Player player) : base(player) { }

        public override void Execute()
        {
            Move(Vector3.up);
        }
    }

    public class MoveDown : PlayerMove
    {
        public MoveDown() : base() { }
        public MoveDown(Player player) : base(player) { }

        public override void Execute()
        {
            Move(Vector3.down);
        }
    }

    public class MoveLeft : PlayerMove
    {
        public MoveLeft() : base() { }
        public MoveLeft(Player player) : base(player) { }

        public override void Execute()
        {
            Move(Vector3.left);
        }
    }

    public class MoveRight : PlayerMove
    {
        public MoveRight() : base() { }
        public MoveRight(Player player) : base(player) { }

        public override void Execute()
        {
            Move(Vector3.right);
        }
    }
}
