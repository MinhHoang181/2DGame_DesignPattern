using DesignPattern.Factory;


namespace DesignPattern.Commands
{
    public abstract class PlayerMove : Command
    {
        private protected Player player;

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
}
