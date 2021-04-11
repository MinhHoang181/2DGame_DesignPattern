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
        private bool isAttack = false;

        protected Player player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            // Observer
            // subrice vao super zombie 
        }

        // Update is called once per frame
        void Update()
        {
            Action();
        }

        public virtual void Action()
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 1f)
            {
                Move();
            }
            else if (!isAttack)
            {
                StartCoroutine(StartAttack());
            }
        }

        public void Setting()
        {

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
            player.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
