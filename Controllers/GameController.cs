using System;
using Demo.Interfaces;
using Demo.Services;
using Demo.Models;
using System.Collections.Generic;

namespace Demo.Controllers
{
  internal class GameController
  {
    private readonly GameService _gameService;
    public GameController()
    {
      _gameService = new GameService();
      Game();

    }

    private bool Playing { get; set; } = true;
    private IRoom DeathRoom { get; set; }
    private IRoom CurrentRoom { get; set; }
  

    private Enemy CurrentEnemy { get; set; }

    private Weapon PlayerWeapon { get; set; }

    void Game()
    {
      System.Console.WriteLine(@"
 ________ ___  ________  ___  ___  _________        ________  ___       ___  ___  ________     
|\  _____\\  \|\   ____\|\  \|\  \|\___   ___\     |\   ____\|\  \     |\  \|\  \|\   __  \    
\ \  \__/\ \  \ \  \___|\ \  \\\  \|___ \  \_|     \ \  \___|\ \  \    \ \  \\\  \ \  \|\ /_   
 \ \   __\\ \  \ \  \  __\ \   __  \   \ \  \       \ \  \    \ \  \    \ \  \\\  \ \   __  \  
  \ \  \_| \ \  \ \  \|\  \ \  \ \  \   \ \  \       \ \  \____\ \  \____\ \  \\\  \ \  \|\  \ 
   \ \__\   \ \__\ \_______\ \__\ \__\   \ \__\       \ \_______\ \_______\ \_______\ \_______\
    \|__|    \|__|\|_______|\|__|\|__|    \|__|        \|_______|\|_______|\|_______|\|_______|
      ");
      Setup();
    }

    void Setup()
    {

      
  
      // Do the things like create enemies and rooms and assign items to enemies
      var Goblin = new Enemy("The Goblin Witch", 25);
      var Knight = new Enemy("The Rusty Knight", 50);
      var ManBearPig = new Enemy("THe MAN BEAR PIG", 200);
      
      var Sword = new Weapon("The Sword", 20, 15);
      var MorningStar = new Weapon("The Morning Star", 20, 25);
      // var knight = new Enemy();
      // var monster = new Enemy();
      // var empty = new Enemy();


      PlayerWeapon = new Weapon("The Hammer", 25, 20);

      var room1 = new Room("The Starting Room", "You awake to the sound of violence coming from the north, west to the south it is suspiciously quiet, An acrid smell permeates from the south.");
      var room2 = new Room("North Room", "its bland but there is a grotesque goblin staring at you");
      var room3 = new Room("The West Wing", "You find yourself in a dark space lit by candles, something dark shuffles in the corner...");
      var poisonTrapRoom = new TrapRoom("Poison Room", "smells bad", 300);
      room2.Enemy = (IEnemy)Goblin;
      room3.Enemy = (IEnemy)Knight;
      Knight.EquipWeapon(MorningStar);
      Goblin.EquipWeapon(Sword);

      DeathRoom = poisonTrapRoom;
      room1.Exits.Add("north", room2);
      room1.Exits.Add("south", DeathRoom);
      room1.Exits.Add("west", room3);
      room2.Exits.Add("south", room1);
      room3.Exits.Add("east", room1);
      


  
      CurrentRoom = room1;
      CurrentEnemy = Goblin;

      Start();
    }

    void Start()
    {
      // Get Player Info
      System.Console.WriteLine("Welcome To The DUNGEON");
      System.Console.WriteLine("What Is Your Name?");
       string playerName = Console.ReadLine();
       _gameService.CreatePlayer(playerName);
      _gameService.CurrentPlayer.EquipWeapon(PlayerWeapon);

      Play();
    }

    void Play()
    {
      // && _gameService.GameState.Playing
      while (!_gameService.CurrentPlayer.Dead )
      {
        System.Console.WriteLine(CurrentRoom.Name);
        System.Console.WriteLine(CurrentRoom.Description);

        _gameService.Messages.Add(new Message("The Dungeon", "What would you like to do?"));
        PrintMessages();
        HandlePlayerInput();
        // Console.Clear();
      }
    }

    private void PrintMessages()
    {
       Console.Clear();
       List<Message> messages = _gameService.Messages;
       for (int i = messages.Count > 5 ? messages.Count - 5 : 0; i < messages.Count; i++)
        {
              Message m = messages[i];
              System.Console.WriteLine($"{m.From}: {m.Content}");
        }
        }
  

    void Go(string direction)
    {
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = CurrentRoom.Exits[direction];
      }
      else
      {
        CurrentRoom = DeathRoom;
      }
      _gameService.Messages.Add(new Message("The Dungeon", "YOU ENTER....."));
      _gameService.Messages.Add(new Message($"The Dungeon", CurrentRoom.Description));
       CurrentEnemy = (Enemy)CurrentRoom.Enemy;
     
    }

    void Attack()
    {
      CurrentEnemy.TakeDamage(_gameService.CurrentPlayer.Weapon.Damage);
      _gameService.CurrentPlayer.TakeDamage(CurrentEnemy.Weapon.Damage);
       _gameService.Messages.Add(new Message("The Dungeon", "You Strike!"));
      _gameService.Messages.Add(new Message("The Dungeon", "The Goblins Health is " + CurrentEnemy.Health + " & Your Health is " + _gameService.CurrentPlayer.Health));

      if(CurrentEnemy.Health <= 0){
        _gameService.Messages.Add(new Message("The Dungeon", $"The {CurrentEnemy} IS DEAD"));
      };


    }

    private void HandlePlayerInput()
    {
      var playerInput = Console.ReadLine();

      if (playerInput == null)
      {
        HandlePlayerInput();
        return;
      }

      var command = playerInput.Split(" ") [0];
      var option = playerInput.Substring(playerInput.IndexOf(" ") + 1);

      switch (command)
      {
        case "go":
          Go(option);
          break;
        case "restart":
          Setup();
          break;
        case "help":
          // Help(); print all the comands
          _gameService.Messages.Add(new Message("The Dungeon", "Bah no help here"));
          break;
        case "fight":
          Attack();
        // _gameService.Messages.Add(new Message("The Dungeon", "You attempt to strike"));
          break;
        case "q":
          Playing = false;
          break;
        default:
        _gameService.Messages.Add(new Message("The Dungeon", "Wrong Answer, Try Again..."));
        break;
      }
    }


  }

}