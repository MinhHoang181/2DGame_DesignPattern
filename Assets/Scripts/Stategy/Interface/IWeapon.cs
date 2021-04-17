using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Strategy
{
    public interface IWeapon
    {
        int Damage { get; set; }
        int KnockBackStrength { get; set; }
        Transform ShootPoint { get; set; }
        GameObject Weapon { get; }

        void Shoot();
    }
}
