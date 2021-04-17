using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Commands;
using DesignPattern.Strategy;
using TMPro;

namespace DesignPattern.Factory
{
    public class Player : MonoBehaviour, Character
    {
        Command btnUp, btnLeft, btnDown, btnRight, btnAttack, btnChangeWeapon;
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
            btnUp = new MoveForward(this);
            btnLeft = new MoveLeft(this);
            btnDown = new MoveBack(this);
            btnRight = new MoveRight(this);
            btnAttack = new PlayerAttack(this);
            btnChangeWeapon = new PlayerChangeWeapon(this);
        }

        public void Start()
        {
            Setting();

            weapon = transform.GetComponent<WeaponController>();
        }

        public void Update()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                btnAttack.Execute();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                btnChangeWeapon.Execute();
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
                btnUp.Execute();
            }
            else if (Input.GetKey(KeyCode.A))
            {  //horizon, verticle
                btnLeft.Execute();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                btnDown.Execute();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                btnRight.Execute();
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

        public void TakeDamage(int damage, float knockBackStrength, Direction directionTakeDamage)
        {
            // bi danh/trung dan
            CurrentHealth -= damage;
        }

        public void Die()
        {
            Debug.Log("Game over");
        }
    }
}
