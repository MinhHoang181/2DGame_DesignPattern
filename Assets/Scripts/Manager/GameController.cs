using System;
using UnityEngine;
using DesignPattern.Factory;
using Cinemachine;

namespace DesignPattern
{
    public class GameController : MonoBehaviour
    {
        #region SINGELTON
        private GameController() { }
        public static GameController Instance { get; private set; }

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

        #region DELEGATES
        public Action<Player> playerChangedEvent;
        public Action<bool> gameStateChangedEvent;
        #endregion

        public bool Debug { get { return debug; } }
        [SerializeField] bool debug;

        [SerializeField] GameObject playerCamera;
        private CinemachineConfiner cameraBound;

        public Player Player { get { return player; } }
        private Player player;
        public bool IsPause { get { return isPause; } }
        private bool isPause;

        // Start is called before the first frame update
        void Start()
        {
            cameraBound = playerCamera.GetComponent<CinemachineConfiner>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                if (player)
                {
                    playerChangedEvent(player);
                }
            }
        }

        public void ChangeState()
        {
            if (isPause)
            {
                isPause = false;
            } else
            {
                isPause = true;
            }
            gameStateChangedEvent(isPause);
        }

        public void UpdateCamera()
        {
            cameraBound.InvalidatePathCache();
        }
    }
}

