using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Strategy
{
    public interface IWeapon
    {
        int Damage { get; set; }
        Transform ShootPoint { get; set; }

        void Shoot();
    }
}
