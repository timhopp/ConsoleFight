using System;
using System.Collections.Generic;
using Demo.Interfaces;

namespace Demo.Models
{
  class Enemy : ICharacter, IEnemy
  {
    public List<IItem> Inventory { get; set; }

    public string Name { get; set; }

    public int Health { get; set; }

    public bool Dead { get => Health <= 0; }

    public IWeapon Weapon { get; set; }

    public List<IItem> Loot => new List<IItem>();

    // public void DealDamage(IEnemy player)
    // {
    //   player.Health -= TakeDamage;
    // }

    public void EquipWeapon(IWeapon weapon) => Weapon = weapon;

    public void TakeDamage(int amount)
    {
      Health -= amount;
    }

    public Enemy(string name, int health)
    {
      Health = health;
      Name = name;
    }

  }

}