using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Commands
{
    public abstract class PlayerMove : Command
    {
        public Player player;

        public PlayerMove() { }

        public PlayerMove(Player player)
        {
            this.player = player;
        }

        public override void Execute()
        {
            Move();
        }

        public abstract void Move();
    }

    public class MoveUp : PlayerMove
    {
        public MoveUp() : base() { }
        public MoveUp(Player player) : base(player)
        {

        }

        public override void Move()
        {
            player.transform.Translate(Vector3.up * player.Speed * Time.deltaTime * 5f);
        }
    }

    public class MoveDown : PlayerMove
    {
        public MoveDown() : base() { }
        public MoveDown(Player player) : base(player)
        {

        }
        public override void Move()
        {
            player.transform.Translate(Vector3.up * -player.Speed * Time.deltaTime * 5f);
        }
    }

    public class MoveLeft : PlayerMove
    {
        public MoveLeft() : base() { }
        public MoveLeft(Player player) : base(player)
        {

        }

        public override void Move()
        {
            player.transform.Translate(Vector3.right * -player.Speed * Time.deltaTime * 5f);
        }
    }

    public class MoveRight : PlayerMove
    {
        public MoveRight() : base() { }
        public MoveRight(Player player) : base(player)
        {

        }

        public override void Move()
        {
            player.transform.Translate(Vector3.right * player.Speed * Time.deltaTime * 5f);
        }
    }
}
