using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class Zombie : MonoBehaviour, Character
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
                    // Chet
                    Die();
                }
            }
        }
        public float Speed { get { return speed; } set { speed = value; } }

        [Header("Stats")]
        [SerializeField] protected int health;
        private int currentHealth;
        [SerializeField] protected float speed;
        [SerializeField] int damage;
        [SerializeField] float timeToAttack;
        [SerializeField] float timeStun;

        [Header("UI")]
        [SerializeField] TextMeshPro healthText;

        private bool isAttack = false;
        private bool isTakeDamage = false;

        protected Player player;
        private Rigidbody2D rigBody;
        protected Coroutine stunCoroutine;

        #region PATHFINDING VALUES

        #endregion

        private void Awake()
        {
            Setting();
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            rigBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }

        public virtual void Action()
        {
            
        }

        public void Setting()
        {
            CurrentHealth = Health;

            healthText.text = CurrentHealth + "/" + Health;    
        }

        private IEnumerator StartAttack()
        {
            isAttack = true;
            yield return new WaitForSeconds(timeToAttack);
            Attack();
            isAttack = false;
        }

        public void Attack()
        {
            player.TakeDamage(damage, 0, Vector2.zero);
        }

        public void TakeDamage(int damage,float knockBackStrength , Vector2 direction)
        {
            CurrentHealth -= damage;

            KnockBack(knockBackStrength, direction);

            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            stunCoroutine = StartCoroutine(OnStunned(timeStun));
        }

        public void KnockBack(float forceStrength, Vector2 direction)
        {
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        IEnumerator OnStunned(float second)
        {
            isTakeDamage = true;
            yield return new WaitForSeconds(second);
            isTakeDamage = false;
        }

        #region PATHFINDING

        public void Move()
        {
        }

        #endregion 
    }
}
