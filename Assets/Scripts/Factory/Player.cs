using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Strategy;
using TMPro;

namespace DesignPattern.Factory
{
    public class Player : MonoBehaviour, Character
    {
        public ScriptableCharacter ScriptableCharacter { get { return scriptablePlayer; } }
        public int Health { get { return health; } }
        public int CurrentHealth { get { return currentHealth; } }
        public float Speed { get { return speed; } }
        public WeaponController Weapon { get { return weapon; } }
        
        public SpriteRenderer Sprite { get { return sprite; } }
        public TextMeshPro HealthText { get { return healthText; } }
        public TextMeshPro WeaponText { get { return weaponText; } }

        [SerializeField] ScriptablePlayer scriptablePlayer;
        private int health;
        private int currentHealth;
        private float speed;
        private float timeStun;

        private TextMeshPro healthText;
        private TextMeshPro weaponText;
        private SpriteRenderer sprite;

        private Transform attackPoint;
        private WeaponController weapon;
        private Rigidbody2D rigBody;

        private bool isTakeDamage = false;
        private Vector3 directionMove;

        private Coroutine stunCoroutine;

        public void Start()
        {
            weapon = transform.GetComponent<WeaponController>();
            rigBody = transform.GetComponent<Rigidbody2D>();
            // Sprite
            sprite = transform.Find("Sprite").transform.GetComponent<SpriteRenderer>();
            attackPoint = sprite.transform.Find("Attack Point").transform;
            // UI
            Transform UITranform = transform.Find("UI").transform;
            healthText = UITranform.Find("Health").GetComponent<TextMeshPro>();
            weaponText = UITranform.Find("Weapon").GetComponent<TextMeshPro>();

            Setting();
        }

        public void Update()
        {
            Action();
        }

        public void Setting()
        {
            // Stats
            health = scriptablePlayer.health;
            currentHealth = scriptablePlayer.health;

            GameController.Instance.playerHealthChangedEvent(this);

            speed = scriptablePlayer.speed;
            timeStun = scriptablePlayer.timeStun;
            // WeaponController
            weapon.AddWeapon(scriptablePlayer.weapon);
            // Sprite
            sprite.sprite = scriptablePlayer.characterSprite;
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

        public void TakeDamage(int damage, float pushBackStrength, Vector2 direction)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);

            GameController.Instance.playerHealthChangedEvent(this);

            if (currentHealth.Equals(0))
            {
                Die();
            }

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

            sprite.transform.right = direction;
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
