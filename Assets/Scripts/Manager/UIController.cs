using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
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
            GameController.Instance.weaponChangedEvent += UpdateWeaponUI;
            GameController.Instance.playerHealthChangedEvent += UpdatePlayerHealthUI;
        }
        private void OnDisable()
        {
            GameController.Instance.weaponChangedEvent -= UpdateWeaponUI;
            GameController.Instance.playerHealthChangedEvent -= UpdatePlayerHealthUI;
        }

        private void UpdateWeaponUI(Character character, ScriptableWeapon weapon)
        {
            character.Sprite.sprite = weapon.weaponSprite;

            Player player = character as Player;
            
            if (player)
            {
                player.WeaponText.text = weapon.weaponName;
            }
        }
        private void UpdatePlayerHealthUI(Player player)
        {
            player.HealthText.text = player.CurrentHealth + "/" + player.Health;
        }
    }
}
