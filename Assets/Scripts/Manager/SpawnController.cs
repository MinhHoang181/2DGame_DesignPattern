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
        [SerializeField] GameObject characterPrefab;

        [Header("Scriptable Object")]
        [SerializeField] ScriptableZombie scriptableZombie;

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = CharacterFactory.CreateCharacter(GameController.Instance.ScriptablePlayer);
            player.transform.position = new Vector3(4, 4, 0);

            SpawnZombie(5);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void SpawnZombie(int number)
        {
            for (int i = 0; i < number; i++)
            {
                int randIndex = Random.Range(0, spawnPoints.Length);
                Vector3 randOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                Vector3 randPosition = spawnPoints[randIndex].position + randOffset;
                GameObject zombie = CharacterFactory.CreateCharacter(scriptableZombie);
                zombie.transform.position = randPosition;
            }
        }
    }
}

