using System.Collections;
using System.Collections.Generic;
using Pathfinding;
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
        protected int currentHealth;
        [SerializeField] protected float speed;
        [SerializeField] protected int damage;
        [SerializeField] protected float attackSpeed;
        [SerializeField] protected int pushBackStrength;
        [SerializeField] protected float timeStun;

        [Header("UI")]
        [SerializeField] TextMeshPro healthText;

        protected bool isAttack = false;
        protected bool isTakeDamage = false;

        protected Player player;
        protected Vector3 playerPosition;
        protected Rigidbody2D rigBody;
        protected Transform sprite;

        protected Coroutine stunCoroutine;
        protected Coroutine attackCoroutine;

        #region PATHFINDING VALUES
        protected List<PathNode> pathNodes = new List<PathNode>();
        #endregion

        protected void Awake()
        {
            Setting();
        }

        // Start is called before the first frame update
        protected void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerPosition = transform.position;
            rigBody = transform.GetComponent<Rigidbody2D>();
            sprite = transform.Find("Sprite").transform;

            InvokeRepeating(nameof(SearchPlayer), 0f, 1f);
        }

        protected void Update()
        {
            Action();

            if (pathNodes.Count > 0 && GameController.Instance.Debug)
            {
                DrawWayToTarget();
            }
        }

        protected virtual void Action()
        {
            if (isTakeDamage) return;
            if (isAttack) return;
            Move();
        }

        public void Setting()
        {
            CurrentHealth = Health;

            healthText.text = CurrentHealth + "/" + Health;    
        }

        protected IEnumerator StartAttack(Vector3 direction)
        {
            isAttack = true;
            yield return new WaitForSeconds(attackSpeed);
            Attack(direction);
            isAttack = false;
        }

        public void Attack(Vector3 direction)
        {
            player.TakeDamage(damage, pushBackStrength, direction);
        }

        public void TakeDamage(int damage,float pushBackStrength , Vector2 direction)
        {
            CurrentHealth -= damage;

            KnockBack(pushBackStrength, direction);

            if (stunCoroutine != null)
            {
                StopCoroutine(stunCoroutine);
            }
            stunCoroutine = StartCoroutine(OnStunned(timeStun));
        }

        public void KnockBack(float pushBackStrength, Vector2 direction)
        {
            rigBody.velocity = Vector2.zero;
            Vector2 force = direction * pushBackStrength * 50;
            rigBody.AddForce(force);
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
        protected void SearchPlayer()
        {
            if (playerPosition != player.transform.position)
            {
                playerPosition = player.transform.position;
                pathNodes = Pathfinding.Pathfinding.Findpath(MapController.Instance.Grid, transform.position, playerPosition, DirectionType.FOUR_DIRECTIONS);
                //Debug.Log("pathNodes: " + pathNodes.Count);
            }
        }

        public void Move()
        {
            if (pathNodes.Count > 0)
            {
                PathNode currentNode = pathNodes[0];
                if (currentNode == null) return;
                Vector3 targetPosition = MapController.Instance.Grid.GetWorldPosition(currentNode.X, currentNode.Y);
                if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
                {
                    Vector3 moveDir = (targetPosition - transform.position).normalized;
                    Vector2 force = moveDir * speed * Time.deltaTime;
                    rigBody.velocity += force;
                    sprite.right = moveDir;
                } else
                {
                    pathNodes.RemoveAt(0);
                }
            }
        }
        #endregion

        #region DEBUG
        // Debug 
        void OnDrawGizmos()
        {
        }

        void DrawWayToTarget()
        {
            Vector2 from = new Vector2(pathNodes[0].X, pathNodes[0].Y);
            Vector2 to = from;
            for (int i = 1; i < pathNodes.Count; i++)
            {
                to.x = pathNodes[i].X;
                to.y = pathNodes[i].Y;
                Debug.DrawLine(from, to, Color.blue);
                from = to;
            }
        }
        #endregion

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("Player"))
            {
                rigBody.velocity = Vector2.zero;
                pathNodes.Clear();

                Vector3 direction = (player.transform.position - transform.position).normalized;
                StartCoroutine(StartAttack(direction));
            }
        }
    }
}
