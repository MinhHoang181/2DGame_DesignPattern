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

        // Start is called before the first frame update
        void Start()
        {
            GameController.Instance.weaponChangedEvent += UpdateWeaponUI;
            GameController.Instance.playerHealthChangedEvent += UpdatePlayerHealthUI;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void UpdateWeaponUI(WeaponScriptableObject weapon)
        {
            Player player = GameController.Instance.Player;
            player.sprite.sprite = weapon.weaponSprite;
            player.weaponText.text = weapon.weaponName;

        }
        private void UpdatePlayerHealthUI()
        {
            Player player = GameController.Instance.Player;
            player.healthText.text = player.CurrentHealth + "/" + player.Health;
        }
    }
}
