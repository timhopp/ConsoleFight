using System;
using System.Collections.Generic;
using Demo.Models;

namespace Demo.Services
{
  internal class GameService
  {
    public GameState GameState { get; internal set; } = new GameState();
    public List<Message> Messages { get; private set; } = new List<Message>();
    public int count { get; private set; }

    public Player CurrentPlayer { get; set; }


    internal void CreatePlayer(string playerName)
    {
      Player player = new Player(playerName);
      Messages.Add(new Message("Guard", $"Welcome to the castle, {player.Name}"));
      CurrentPlayer = player;
    }

    internal void AddMessage()
    {
      count++;
    
      Messages.Add(new Message("System", $"User took action number {count}"));
    }
  }






}