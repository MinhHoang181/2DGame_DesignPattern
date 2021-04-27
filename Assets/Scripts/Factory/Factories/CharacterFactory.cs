using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Factory
{
    public abstract class CharacterFactory
    {
        protected GameObject prefab;

        public CharacterFactory(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public GameObject Create()
        {
            return MakeCharacter();
        }

        protected abstract GameObject MakeCharacter();
    }
}

