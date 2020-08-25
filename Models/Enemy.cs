using System;
using System.Collections.Generic;
using Demo.Interfaces;

namespace Demo.Models
{
  class Enemy : ICharacter
  {
    public List<IItem> Inventory { get; set; }

    public string Name { get; set; }

    public int Health { get; set; }

    public bool Dead { get => Health <= 0; }

    public IWeapon Weapon { get; set; }

    public void DealDamage(IEnemy player)
    {
      throw new NotImplementedException();
    }

    public void EquipWeapon(IWeapon weapon) => Weapon = weapon;

    public void TakeDamage(int amount)
    {
      Health -= amount;
    }

    public Enemy(string name, int health)
    {
      Console.WriteLine("Arrrgh I'm the Enemy");
      Health = health;
      Name = name;
    }

  }

}