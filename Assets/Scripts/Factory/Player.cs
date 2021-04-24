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
                    Die();
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
        private Vector3 directionMove;

        private Coroutine stunCoroutine;

        public void Start()
        {
            Setting();

            weapon = transform.GetComponent<WeaponController>();
            spriteObject = transform.Find("Sprite");
            rigBody = transform.GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            Action();
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
        }

        public void Move(Vector3 direction)
        {
            if (isTakeDamage) return;

            directionMove = (directionMove + direction).normalized;
            //Debug.Log(directionMove);
            transform.Translate(direction * Speed * Time.deltaTime);
        }

        public void Attack()
        {
            if (isTakeDamage) return;

            Vector2 direction = (weapon.ShootPoint.position - transform.position).normalized;
            weapon.Fire(direction);
        }

        public void ChangeWeapon()
        {
            weapon.Weapon(WeaponType.Bullet);
        }

        public void TakeDamage(int damage, float pushBackStrength, Vector2 direction)
        {
            CurrentHealth -= damage;

            PushBack(pushBackStrength, direction);
            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            stunCoroutine = StartCoroutine(OnStunned(timeStun));
        }

        IEnumerator OnStunned(float second)
        {
            isTakeDamage = true;
            yield return new WaitForSeconds(second);
            isTakeDamage = false;
        }

        public void PushBack(float pushBackStrength, Vector2 direction)
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

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("Zombie"))
            {
                if (isTakeDamage == false)
                {
                    rigBody.velocity = Vector2.zero;
                }
            }
        }
    }
}
