using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class ZombieFactory : CharacterFactory
    {
        public ZombieFactory(GameObject prefab) : base(prefab) { }

        protected override GameObject MakeCharacter()
        {
            GameObject zombie = prefab;
            return zombie;
        }
    }
}

