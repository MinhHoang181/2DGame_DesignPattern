using System;
using UnityEngine;
using DesignPattern.Factory;

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
        public static event Action<Player> PlayerChangedEvent;
        public static event Action<bool> GameStateChangedEvent;
        #endregion
        public ScriptablePlayer ScriptablePlayer { get { return scriptablePlayer; } }
        [SerializeField] ScriptablePlayer scriptablePlayer;
        public bool Debug { get { return debug; } }
        [SerializeField] bool debug;
        public LayerMask HitLayers { get { return hitLayers; } }
        [SerializeField] LayerMask hitLayers;

        

        public Player Player { get { return player; } }
        private Player player;
        public bool IsPause { get { return isPause; } }
        private bool isPause;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
                if (player)
                {
                    PlayerChangedEvent?.Invoke(player);
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
            //gameStateChangedEvent(isPause);
        }

        
    }
}

