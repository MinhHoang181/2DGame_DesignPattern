using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Strategy
{
    public interface IWeapon
    {
        float Speed { get; set; }
        int Damage { get; set; }
        GameObject Weapon { get; set; }
        Player Player { get; set; }

        void Shoot();
    }
}
