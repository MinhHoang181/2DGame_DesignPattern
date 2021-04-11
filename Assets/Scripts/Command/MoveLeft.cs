using DesignPattern.Factory;

namespace DesignPattern.Commands

{
    public class MoveLeft : PlayerMove
    {
        public MoveLeft(Player player) : base(player)
        {

        }

        public override void Move()
        {
            player.transform.Translate(player.transform.right * -player.Speed);
        }
    }
}
