namespace Demo.Models
{
   internal class GameState
   {
     public Player Player { get; private set; }
     public Room CurrentRoom { get; private set; }
     public bool Playing { get; private set; } = true;

     public void SetPlayer(Player player)
     {
       Player = player;
     }
   }
}