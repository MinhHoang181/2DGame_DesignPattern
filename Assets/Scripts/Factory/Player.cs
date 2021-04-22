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

        [Header("Stats")]
        [SerializeField] int health;
        private int currentHealth;
        [SerializeField] float speed;
        [SerializeField] float timeStun;

        [Header("UI")]
        [SerializeField] TextMeshPro healthText;

        private WeaponController weapon;
        private Transform spriteObject;
        private Rigidbody2D rigBody;

        private bool isTakeDamage = false;

        private Coroutine stunCoroutine;

        public void Awake()
        {
            
        }

        public void Start()
        {
            Setting();

            weapon = transform.GetComponent<WeaponController>();
            spriteObject = transform.Find("Sprite");
            rigBody = transform.GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            FacingToMouse();
            Move();
        }

        public void Setting()
        {
            CurrentHealth = Health;
            healthText.text = CurrentHealth + "/" + Health;
        }

        public void Action()
        {
            if (isTakeDamage) return;
            FacingToMouse();
            Move();
        }

        public void Move()
        {
        }

        public void Attack()
        {
            Attack(Vector3.zero);
        }

        public void Attack(Vector3 direction) // 
        {
            //Debug.Log("Ban");
            weapon.Fire();
        }

        public void ChangeWeapon() // Dinh nghia
        {
            weapon.Weapon(WeaponType.Bullet);
        }

        public void TakeDamage(int damage, float pushBackStrength, Vector2 direction)
        {
            // bi danh/trung dan
            CurrentHealth -= damage;

            KnockBack(pushBackStrength, direction);
            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            StartCoroutine(OnStunned(timeStun));
        }

        IEnumerator OnStunned(float second)
        {
            isTakeDamage = true;
            yield return new WaitForSeconds(second);
            isTakeDamage = false;
        }

        public void KnockBack(float pushBackStrength, Vector2 direction)
        {
            rigBody.velocity = Vector2.zero;
            Vector2 force = direction * pushBackStrength * 50;
            rigBody.AddForce(force);
        }

        public void Die()
        {
            Debug.Log("Game over");
        }

        private void FacingToMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

            spriteObject.right = direction;
        }
    }
}
