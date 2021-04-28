using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using DesignPattern.Strategy;
using UnityEngine;

namespace DesignPattern
{
    public class UIController : MonoBehaviour
    {
        #region SINGELTON
        private UIController() { }
        public static UIController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        #endregion

        private void OnEnable()
        {
            WeaponController.OnWeaponChange += UpdateWeaponUI;
            Character.OnHealthChange += UpdateHealthUI;
        }
        private void OnDisable()
        {
            WeaponController.OnWeaponChange -= UpdateWeaponUI;
            Character.OnHealthChange -= UpdateHealthUI;
        }

        private void UpdateWeaponUI(Character character, ScriptableWeapon weapon)
        {
            character.Sprite.sprite = weapon.weaponSprite;
        }

        private void UpdateHealthUI(Character character)
        {
            character.HealthText.text = character.CurrentHealth + "/" + character.Health;
        }
    }
}
