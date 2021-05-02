using System;
using UnityEngine;
using DesignPattern.Factory;
using UnityEngine.SceneManagement;

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
        public static event Action StartGameEvent;
        public static event Action GameStateChangedEvent;
        public static event Action GameOverEvent;
        #endregion

        public ScriptablePlayer ScriptablePlayer { get { return scriptablePlayer; } }
        [SerializeField] ScriptablePlayer scriptablePlayer;
        public bool Debug { get { return debug; } }
        [SerializeField] bool debug;
        public LayerMask HitLayers { get { return hitLayers; } }
        [SerializeField] LayerMask hitLayers;

        #region SCENES
        [Header("Scenes")]
        [SerializeField] string mainScene;
        [SerializeField] string gameScene;
        #endregion

        public Player Player { get { return player; } }
        private Player player;
        public bool IsPause { get { return isPause; } }
        private bool isPause;
        public bool IsPlaying { get { return isPlaying; } }
        private bool isPlaying;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void AssignPlayer(Player player)
        {
            if (this.player != null && this.player != player)
            {
                Destroy(player.gameObject);
            }
            this.player = player;
            player.OnDie += GameOver;
            PlayerChangedEvent?.Invoke(this.player);
        }

        public void ChangeState()
        {
            if (isPause)
            {
                isPause = false;
                Time.timeScale = 1;
            } else
            {
                isPause = true;
                Time.timeScale = 0;
            }
            GameStateChangedEvent?.Invoke();
        }

        #region SCENES MANAGER
        public void LoadGameScene()
        {
            isPause = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(gameScene);
            isPlaying = true;
            StartGameEvent?.Invoke();
        }

        public void LoadMainScene()
        {
            isPause = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(mainScene);
            isPlaying = false;
            StartGameEvent?.Invoke();
        }
        #endregion

        private void GameOver()
        {
            isPause = true;
            Time.timeScale = 0;
            isPlaying = false;
            GameOverEvent?.Invoke();
        }
    }
}

