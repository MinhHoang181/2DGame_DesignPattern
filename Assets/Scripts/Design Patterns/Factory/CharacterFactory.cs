using System.Collections;
using System.Collections.Generic;
using DesignPattern.Strategy;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class CharacterFactory : MonoBehaviour
    {
        public static GameObject Prefab
        {
            get
            {
                if (prefab == null)
                {
                    prefab = Resources.Load("Prefabs/Characters/Character", typeof(GameObject)) as GameObject;
                }
                return prefab;
            }
        }
        private static GameObject prefab;

        public static GameObject CreateCharacter(ScriptableCharacter scriptableCharacter)
        {
            //GameObject character = Instantiate(Prefab);
            GameObject character = Prefab.Spawn();
            character.name = scriptableCharacter.name;
            RemoveAllComponent(character);

            switch (scriptableCharacter.characterType)
            {
                case CharacterType.Player:
                    SettingPlayerCharacter(character, scriptableCharacter);
                    return character;
                case CharacterType.Zombie:
                    SettingZombieCharacter(character, scriptableCharacter);
                    return character;
                default:
                    //Destroy(character);
                    character.Kill();
                    return null;
            }
        }

        private static void RemoveAllComponent(GameObject gameObject)
        {
            Component character = gameObject.GetComponent<Character>();
            if (character != null)
            {
                Destroy(character);
            }
            Component weaponController = gameObject.GetComponent<WeaponController>();
            if (weaponController != null)
            {
                Destroy(weaponController);
            }
            Component iweapon = gameObject.GetComponent<IWeapon>() as Component;
            if (iweapon != null)
            {
                Destroy(iweapon);
            }
        }

        private static void SettingPlayerCharacter(GameObject character, ScriptableCharacter scriptableCharacter)
        {
            character.tag = "Player";
            character.layer = LayerMask.NameToLayer("Player");
            character.AddComponent<Player>().ScriptableCharacter = scriptableCharacter;
            character.AddComponent<WeaponController>();
        }

        private static void SettingZombieCharacter(GameObject character, ScriptableCharacter scriptableCharacter)
        {
            character.tag = "Zombie";
            character.layer = LayerMask.NameToLayer("Zombie");
            character.AddComponent<Zombie>().ScriptableCharacter = scriptableCharacter;
        }
    }
}

