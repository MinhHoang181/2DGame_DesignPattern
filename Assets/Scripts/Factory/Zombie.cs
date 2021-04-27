using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class Zombie : Character
    {
        protected ScriptableZombie scriptableZombie;
        protected int damage;
        protected float timeToAttack;
        protected int pushBackStrength;

        // Boolean
        protected bool isAttack = false;
        
        protected Vector3 playerPosition;
        protected Coroutine attackCoroutine;

        #region PATHFINDING VALUES
        protected List<PathNode> pathNodes = new List<PathNode>();
        #endregion

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            Setting();
        }

        protected void Update()
        {
            Action();

            if (pathNodes.Count > 0 && GameController.Instance.Debug)
            {
                DrawWayToTarget();
            }
        }

        protected override void Setting()
        {
            base.Setting();
            scriptableZombie = (ScriptableZombie)scriptableCharacter;
            damage = scriptableZombie.damage;
            timeToAttack = scriptableZombie.timeToAttack;

            playerPosition = transform.position;
            // setting loop search player
            InvokeRepeating(nameof(SearchPlayer), 0f, 1f);
        }

        protected void Action()
        {
            if (isTakeDamage) return;
            if (isAttack) return;
            AIMove();
        }

        public override void Move(Vector3 direction)
        {
            Vector2 force = direction * Speed * Time.deltaTime;
            rigBody.velocity += force;
            sprite.transform.right = direction;
        }

        protected IEnumerator StartAttack()
        {
            isAttack = true;
            yield return new WaitForSeconds(timeToAttack);
            Attack();
            isAttack = false;
        }

        public override void Attack()
        {
            Vector2 direction = (attackPoint.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, direction, 0.1f);
            if (hit)
            {
                Player player = hit.collider.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(damage, pushBackStrength, direction);
                }
            }
        }

        protected override void Die()
        {
            CancelInvoke();

            Destroy(gameObject);
        }

        #region PATHFINDING
        protected void SearchPlayer()
        {
            Player player = GameController.Instance.Player;
            if (playerPosition != player.transform.position)
            {
                playerPosition = player.transform.position;
                pathNodes = Pathfinding.Pathfinding.Findpath(MapController.Instance.Grid, transform.position, playerPosition, DirectionType.FOUR_DIRECTIONS);
                //Debug.Log("pathNodes: " + pathNodes.Count);
            }
        }

        private void AIMove()
        {
            if (pathNodes.Count > 0)
            {
                PathNode currentNode = pathNodes[0];
                if (currentNode == null) return;
                Vector3 targetPosition = MapController.Instance.Grid.GetWorldPosition(currentNode.X, currentNode.Y);
                if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
                {
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    Move(direction);
                    
                } else
                {
                    pathNodes.RemoveAt(0);
                }
            }
        }
        #endregion

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("Player"))
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                sprite.transform.right = direction;
                
                if (isAttack == false)
                {
                    StartCoroutine(StartAttack());
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.tag.Equals("Player"))
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                sprite.transform.right = direction;

                if (isAttack == false)
                {
                    StartCoroutine(StartAttack());
                }
            }
        }

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
    }
}
