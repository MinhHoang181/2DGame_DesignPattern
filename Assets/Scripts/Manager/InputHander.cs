using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Commands;
using DesignPattern.Factory;

namespace DesignPattern
{
    public class InputHander : MonoBehaviour
    {
        #region SINGELTON
        private InputHander() { }
        public static InputHander Instance { get; private set; }

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

        private PlayerCommand btnUp, btnDown, btnLeft, btnRight, btnAttack, btnChangeNextWeapon, btnChangePrevWeapon;

        [Header("Move")]
        [SerializeField] KeyCode up = KeyCode.W;
        [SerializeField] KeyCode down = KeyCode.S;
        [SerializeField] KeyCode left = KeyCode.A;
        [SerializeField] KeyCode right = KeyCode.D;
        [Header("Weapon")]
        [SerializeField] KeyCode attack = KeyCode.Mouse0;
        [SerializeField] KeyCode changeNext = KeyCode.Q;
        [SerializeField] KeyCode changePrevious = KeyCode.E;
        [SerializeField] KeyCode reload = KeyCode.R;
        [Header("Setting")]
        [SerializeField] KeyCode pause = KeyCode.Escape;

        private void OnEnable()
        {
            GameController.PlayerChangedEvent += UpdateButton;
        }
        private void OnDisable()
        {
            GameController.PlayerChangedEvent -= UpdateButton;
        }

        // Start is called before the first frame update
        void Start()
        {
            btnUp = new MoveUp(GameController.Instance.Player);
            btnLeft = new MoveLeft(GameController.Instance.Player);
            btnDown = new MoveDown(GameController.Instance.Player);
            btnRight = new MoveRight(GameController.Instance.Player);
            btnAttack = new PlayerAttack(GameController.Instance.Player);
            btnChangeNextWeapon = new PlayerChangeNextWeapon(GameController.Instance.Player);
            btnChangePrevWeapon = new PlayerChangePrevioustWeapon(GameController.Instance.Player);
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameController.Instance.IsPause)
            {
                if (GameController.Instance.Player != null)
                {
                    PlayerControl();
                }
            }

            if (Input.GetKeyDown(pause))
            {
                GameController.Instance.ChangeState();
            }

            if (GameController.Instance.IsPause)
            {
                SettingControl();
            }
        }

        private void UpdateButton(Player player)
        {
            btnUp.player = player;
            btnDown.player = player;
            btnLeft.player = player;
            btnRight.player = player;
            btnAttack.player = player;
            btnChangeNextWeapon.player = player;
            btnChangePrevWeapon.player = player;
        }

        private void PlayerControl()
        {
            // MOVE
            if (Input.GetKey(up))
            {
                btnUp.Execute();
            }
            if (Input.GetKey(down))
            {
                btnDown.Execute();
            }
            if (Input.GetKey(left))
            {
                btnLeft.Execute();
            }
            if (Input.GetKey(right))
            {
                btnRight.Execute();
            }

            // ATTACK
            if (Input.GetKey(attack))
            {
                btnAttack.Execute();
            }
            if (Input.GetKeyDown(reload))
            {
                Debug.Log("reload");
            }
            if (Input.GetKeyDown(changeNext))
            {
                btnChangeNextWeapon.Execute();
            }
            if (Input.GetKeyDown(changePrevious))
            {
                btnChangePrevWeapon.Execute();
            }
        }

        private void SettingControl()
        {

        }
    }
}
