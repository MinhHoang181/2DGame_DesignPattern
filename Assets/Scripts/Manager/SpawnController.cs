using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;
using System;
using Random = UnityEngine.Random;

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
        [Header("Number Zombie")]
        [SerializeField] int numberZombie;
        private int currentNumberZombie;

        [Header("Spawn Point")]
        [SerializeField] Transform playerSpawnPoint;
        [SerializeField] Transform[] spawnPoints;

        [Header("Prefab")]
        [SerializeField] GameObject characterPrefab;

        [Header("Scriptable Object")]
        [SerializeField] ScriptableZombie scriptableZombie;

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = CharacterFactory.CreateCharacter(GameController.Instance.ScriptablePlayer);
            player.transform.position = playerSpawnPoint.position;

            SpawnZombie(numberZombie);
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

                currentNumberZombie++;
                zombie.GetComponent<Zombie>().OnDie += ZombieDie;
            }
        }

        private void ZombieDie(Character zombie)
        {
            currentNumberZombie -= 1;
            zombie.OnDie -= ZombieDie;

            if (currentNumberZombie <= 0)
            {
                numberZombie += 5;
                SpawnZombie(numberZombie);
            }
        }
    }
}

