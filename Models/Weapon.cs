using System;
using System.Collections.Generic;
using Demo.Interfaces;

namespace Demo.Models
{
  class Weapon : IWeapon
  {
    string IItem.Name { get; set; }
   
    int IItem.Durability { get; set; }
    int IWeapon.Damage { get; set; }

  public Weapon(string name, int durability, int damage)
  {

  }

  }


}