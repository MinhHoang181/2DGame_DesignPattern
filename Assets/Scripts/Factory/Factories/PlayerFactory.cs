using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class PlayerFactory : CharacterFactory
    {
        public PlayerFactory(GameObject prefab) : base(prefab) { }

        protected override GameObject MakeCharacter()
        {
            GameObject player = prefab;
            return player;
        }
    }
}