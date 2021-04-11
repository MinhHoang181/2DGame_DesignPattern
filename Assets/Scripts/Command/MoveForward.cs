using DesignPattern.Factory;


namespace DesignPattern.Commands

{
    public class MoveForward : PlayerMove
    {
        public MoveForward(Player player) : base(player)
        {

        }


        public override void Move()
        {
            player.transform.Translate(player.transform.up * player.Speed);
        }
    }
}


