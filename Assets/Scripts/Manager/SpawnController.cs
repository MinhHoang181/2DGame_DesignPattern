using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern
{
    public class SpawnController : MonoBehaviour
    {
        #region SINGELTON
        private SpawnController() { }
        public static SpawnController Instance { get; private set; }

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
        }
        #endregion
        [Header("Spawn Point")]
        [SerializeField] Transform[] spawnPoints;

        [Header("Prefab")]
        [SerializeField] GameObject playerPrefab;
        [SerializeField] GameObject zombiePrefab;

        private PlayerFactory playerFactory;
        private ZombieFactory zombieFactory;

        // Start is called before the first frame update
        void Start()
        {
            playerFactory = new PlayerFactory(playerPrefab);
            zombieFactory = new ZombieFactory(zombiePrefab);

            GameObject player = playerFactory.Create();
            Instantiate(player, new Vector3(4, 4, 0), Quaternion.identity);

            GameObject zombie = zombieFactory.Create();
            Instantiate(zombie, new Vector3(3, 3, 0), Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void SpawnZombie(GameObject zombie, int number)
        {
            for (int i = 0; i < number; i++)
            {
                int randIndex = Random.Range(0, spawnPoints.Length);
                Vector3 randOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                Vector3 randPosition = spawnPoints[randIndex].position + randOffset;
                Transform newZombie = Instantiate(zombie, randPosition, Quaternion.identity).transform;
            }
        }
    }
}

