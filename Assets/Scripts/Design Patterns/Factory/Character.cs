using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DesignPattern.Factory
{
    public abstract class Character: MonoBehaviour
    {
        #region DELEGATES
        public static event Action<Character> OnHealthChange;
        public event Action<Character> OnDie;
        #endregion

        #region PUBLIC VALUES
        public ScriptableCharacter ScriptableCharacter
        {
            get { return scriptableCharacter; }
            set { scriptableCharacter = value; }
        }
        public int Health { get { return health; } }
        public int CurrentHealth { get { return currentHealth; } }
        public float Speed { get { return speed; } }
        #endregion

        #region PUBLIC UI
        public Transform AttackPoint { get { return attackPoint; } }
        public TextMeshPro HealthText { get { return healthText; } }
        #endregion

        #region PUBLIC COMPONENTS
        public SpriteRenderer Sprite { get { return sprite; } }
        public Rigidbody2D RigBody { get { return rigBody; } }
        #endregion

        #region PRIVATE VALUES
        protected ScriptableCharacter scriptableCharacter;
        protected int health;
        protected int currentHealth;
        protected float speed;
        protected float timeStun;
        #endregion

        protected Transform UITranform;
        protected TextMeshPro healthText;
        protected SpriteRenderer sprite;

        protected Transform attackPoint;
        protected Rigidbody2D rigBody;

        protected Coroutine stunCoroutine;

        #region BOOLEAN
        protected bool isFirsSpawn = true;
        protected bool isTakeDamage = false;
        #endregion

        #region ABSTRACT FUNCTION
        public abstract void Move(Vector3 direction);
        public abstract void Attack();
        #endregion

        protected virtual void OnEnable()
        {
            if (isFirsSpawn) return;

            Setting();
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }

        protected virtual void Start()
        {
            rigBody = transform.GetComponent<Rigidbody2D>();
            // Sprite
            sprite = transform.Find("Sprite").transform.GetComponent<SpriteRenderer>();
            attackPoint = sprite.transform.Find("Attack Point").transform;
            // UI
            UITranform = transform.Find("UI").transform;
            healthText = UITranform.Find("Health").GetComponent<TextMeshPro>();
        }

        protected virtual void Setting()
        {
            isFirsSpawn = false;
            // Stats
            health = ScriptableCharacter.health;
            currentHealth = ScriptableCharacter.health;
            OnHealthChange?.Invoke(this);
            speed = ScriptableCharacter.speed;
            timeStun = ScriptableCharacter.timeStun;
            // Sprite
            sprite.sprite = ScriptableCharacter.characterSprite;
        }

        public virtual void TakeDamage(int damage, float pushBackStrength, Vector2 direction)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);

            OnHealthChange?.Invoke(this);

            if (currentHealth.Equals(0))
            {
                Die();
                return;
            }

            PushBack(pushBackStrength, direction);
            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            stunCoroutine = StartCoroutine(OnStunned(timeStun));
        }

        protected IEnumerator OnStunned(float second)
        {
            isTakeDamage = true;
            yield return new WaitForSeconds(second);
            isTakeDamage = false;
        }

        protected void PushBack(float pushBackStrength, Vector2 direction)
        {
            rigBody.velocity = Vector2.zero;
            Vector2 force = direction * pushBackStrength * 50;
            rigBody.AddForce(force);
        }

        protected virtual void Die()
        {
            OnDie?.Invoke(this);
        }
    }
}
