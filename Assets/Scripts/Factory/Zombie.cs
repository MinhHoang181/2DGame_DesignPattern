using System.Collections;
using System.Collections.Generic;
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

        private bool isAttack = false;
        private bool isTakeDamage = false;

        protected Player player;
        protected Rigidbody2D rigBody;

        protected Coroutine stunCoroutine;

        private void Awake()
        {
            Setting();
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            rigBody = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Action();
        }

        public virtual void Action()
        {
            if (isTakeDamage) return;

            if (Vector3.Distance(transform.position, player.transform.position) > 1f)
            {
                Move();
                return;
            }

            if (!isAttack)
            {
                StartCoroutine(StartAttack());
            }
        }

        public void Setting()
        {
            CurrentHealth = Health;
        }

        public virtual void Move()
        {
            // huong mat ve phia nguoi choi
            transform.LookAt(player.transform.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            // di chuyen toi phia truoc
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
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
            //Debug.Log(transform.name + " attack player");
            player.TakeDamage(damage, 0, Direction.DOWN);
        }

        public void TakeDamage(int damage,float knockBackStrength ,Direction directionTakeDamage)
        {
            CurrentHealth -= damage;

            KnockBack(knockBackStrength ,directionTakeDamage);

            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            stunCoroutine = StartCoroutine(OnStunned(timeStun));
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void KnockBack(float forceStrength ,Direction directionTakeDamage)
        {
            Vector2 direction = Vector2.zero;
            switch (directionTakeDamage)
            {
                case Direction.RIGHT:
                    direction.x = forceStrength;
                    break;
                case Direction.LEFT:
                    direction.x = -forceStrength;
                    break;
                case Direction.UP:
                    direction.y = forceStrength;
                    break;
                case Direction.DOWN:
                    direction.y = -forceStrength;
                    break;
                default:
                    break;
            }
            rigBody.AddForce(direction);
        }

        IEnumerator OnStunned(float second)
        {
            isTakeDamage = true;
            yield return new WaitForSeconds(second);
            isTakeDamage = false;
        }
    }
}
