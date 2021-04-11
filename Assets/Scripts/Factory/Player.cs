using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Commands;
using TMPro;

namespace DesignPattern.Factory
{
    public class Player : MonoBehaviour, Character
    {
        Command btnW, btnA, btnS, btnD;
        public int Health { get { return health; } set { health = value; } }
        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = Mathf.Max(0, value);
                healthText.text = CurrentHealth + "/" + Health;
                if (currentHealth.Equals(0))
                {
                    // Chet
                } 
            }
        }
        public float Speed { get { return speed; } set { speed = value; } }
        public Direction Facing { get { return facing; } set { facing = value; } }

        [Header("Stats")]
        [SerializeField] int health;
        private int currentHealth;
        [SerializeField] float speed;

        [Header("UI")]
        [SerializeField] TextMeshPro healthText;

        private WeaponController weapon;
        private Direction facing = Direction.RIGHT;

        public void Awake()
        {
            btnW = new MoveForward(this);
            btnA = new MoveLeft(this);
            btnS = new MoveBack(this);
            btnD = new MoveRight(this);
        }

        public void Start()
        {
            Setting();
        }

        public void Update()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack(); // Tu lam
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Press C");
                ChangeWeapon(); // Tu lam
            }
        }

        public void Setting()
        {
            CurrentHealth = Health;
            healthText.text = CurrentHealth + "/" + Health;
        }

        public void Move()
        {
            
            if (Input.GetKey(KeyCode.W))
            {
                btnW.Execute();
            }
            else if (Input.GetKey(KeyCode.A))
            {  //horizon, verticle
                btnA.Execute();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                btnS.Execute();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                btnD.Execute();
            }
        }

        public void Attack() // 
        {
            //Debug.Log("Ban");
            weapon.Fire();
        }

        public void ChangeWeapon() // Dinh nghia
        {
            weapon.Weapon(WeaponType.Bullet);
        }

        public void OnDamaged(int damage)
        {
            // bi danh/trung dan
            CurrentHealth -= damage;
        }
    }
}
