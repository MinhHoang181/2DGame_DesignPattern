using UnityEngine;

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

 

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

