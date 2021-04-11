using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class Zombie : MonoBehaviour, Character
    {
        public int Health { get { return health; } set { health = value; } }
        public float Speed { get { return speed; } set { speed = value; } }

        [SerializeField] protected int health;

        [SerializeField] protected float speed;

        protected Transform player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

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
            if (Vector3.Distance(transform.position, player.position) > 1f)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }

        public virtual void Move()
        {
            // huong mat ve phia nguoi choi
            transform.LookAt(player.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            // di chuyen toi phia truoc
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        public void Attack()
        {
            Debug.Log(transform.name + " attack player");
        }
    }
}
