using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Strategy
{
    public interface IWeapon
    {
        int Damage { get;}
        Transform ShootPoint { get; set; }
        LayerMask HitLayers { get; set; }

        WeaponScriptableObject Weapon { get; set; }

        void Shoot(Vector2 direction);
    }
}
