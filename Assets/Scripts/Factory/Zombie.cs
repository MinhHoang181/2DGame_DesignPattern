using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pathfinding;

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

        protected Coroutine stunCoroutine;

        #region PATHFINDING VALUES
        private Seeker seeker;
        private Path path;
        private int currentWaypoint = 0;
        private bool reachedEndOfPath;
        private float nextWaypointDistance = 3;

        #endregion

        private void Awake()
        {
            Setting();
        }

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            seeker = GetComponent<Seeker>();
            seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
        }

        // Update is called once per frame
        void Update()
        {
            Action();
        }

        public virtual void Action()
        {
            if (isTakeDamage) return;

            //if (Vector3.Distance(transform.position, player.transform.position) > 1f)
            //{
            //    Move();
            //    return;
            //}

            //if (!isAttack)
            //{
            //    StartCoroutine(StartAttack());
            //}
            UpdatePath();
            Move();
        }

        public void Setting()
        {
            CurrentHealth = Health;

            healthText.text = CurrentHealth + "/" + Health;
        }

        //public virtual void Move()
        //{
        //    // huong mat ve phia nguoi choi
        //    transform.LookAt(player.transform.position);
        //    transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        //    // di chuyen toi phia truoc
        //    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        //}

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

        public void Die()
        {
            Destroy(gameObject);
        }

        public void KnockBack(float forceStrength, Vector2 direction)
        {
        }

        IEnumerator OnStunned(float second)
        {
            isTakeDamage = true;
            yield return new WaitForSeconds(second);
            isTakeDamage = false;
        }

        #region PATHFINDING
        void UpdatePath()
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(transform.position, player.transform.position, OnPathComplete);
            }
        }

        public void OnPathComplete(Path p)
        {
            Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

            if (!p.error)
            {
                path = p;
                // Reset the waypoint counter so that we start to move towards the first point in the path
                currentWaypoint = 0;
            }
        }

        public void Move()
        {
            if (path == null)
            {
                // We have no path to follow yet, so don't do anything
                return;
            }

            // Check in a loop if we are close enough to the current waypoint to switch to the next one.
            // We do this in a loop because many waypoints might be close to each other and we may reach
            // several of them in the same frame.
            reachedEndOfPath = false;
            // The distance to the next waypoint in the path
            float distanceToWaypoint;
            while (true)
            {
                // If you want maximum performance you can check the squared distance instead to get rid of a
                // square root calculation. But that is outside the scope of this tutorial.
                distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distanceToWaypoint < nextWaypointDistance)
                {
                    // Check if there is another waypoint or if we have reached the end of the path
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        // Set a status variable to indicate that the agent has reached the end of the path.
                        // You can use this to trigger some special code if your game requires that.
                        reachedEndOfPath = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            // Slow down smoothly upon approaching the end of the path
            // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
            var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

            // Direction to the next waypoint
            // Normalize it so that it has a length of 1 world unit
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            // Multiply the direction by our desired speed to get a velocity
            Vector3 velocity = dir * speed * speedFactor;

            // If you are writing a 2D game you should remove the CharacterController code above and instead move the transform directly by uncommenting the next line
            transform.position += velocity * Time.deltaTime;
            //transform.Translate(velocity * Time.deltaTime);
        }

        #endregion 
    }
}
