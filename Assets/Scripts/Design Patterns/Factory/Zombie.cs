using System;
using System.Collections;
using System.Collections.Generic;
using DesignPattern.State;
using Pathfinding;
using TMPro;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class Zombie : Character
    {
        public List<PathNode> PathNodes { get { return pathNodes; } set { pathNodes = value; } }
        public Vector3 TargetPosition { get { return targetPosition; } set { targetPosition = value; } }
        public float TimeToAttack { get { return timeToAttack; } }

        public bool IsAttack { get { return isAttack; } set { isAttack = value; } }

        protected ScriptableZombie scriptableZombie;
        protected int damage;
        protected float timeToAttack;
        protected float pushBackStrength;

        // Boolean
        protected bool isAttack = false;
        
        protected Vector3 targetPosition;
        protected Coroutine attackCoroutine;

        #region PATHFINDING VALUES
        protected List<PathNode> pathNodes = new List<PathNode>();
        #endregion

        private StateMachine stateMachine;

        #region STATE PATTERN
        private void Awake()
        {
            stateMachine = new StateMachine();

            var search = new SearchState(this);
            var move = new MoveState(this);
            var attack = new AttackState(this);

            // SEARCH -> MOVE
            stateMachine.AddTransition(search, move, HasTarget());
            // MOVE -> SEARCH
            stateMachine.AddTransition(move, search, HasNoTarget());
            stateMachine.AddTransition(move, search, StuckForASecond());
            // MOVE -> ATTACK
            stateMachine.AddTransition(move, attack, ReachRangeToAttack());
            // ATTACK -> SEARCH
            stateMachine.AddTransition(attack, search, OutRangeToAttack());

            stateMachine.SetState(search);

            Func<bool> HasTarget() => () => pathNodes.Count > 0;
            Func<bool> HasNoTarget() => () => pathNodes.Count == 0; //|| targetPosition != GameController.Instance.Player.transform.position;
            Func<bool> StuckForASecond() => () => move.TimeStuck > 1f;
            Func<bool> ReachRangeToAttack() => () => Vector3.Distance(transform.position, GameController.Instance.Player.transform.position) < 1f;
            Func<bool> OutRangeToAttack() => () => Vector3.Distance(transform.position, GameController.Instance.Player.transform.position) > 1f;
        }

        #endregion
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            Setting();
        }

        protected void Update()
        {
            stateMachine.Tick();


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
            pushBackStrength = scriptableZombie.pushBackStrength;

            targetPosition = transform.position;
        }

        public override void Move(Vector3 direction)
        {
            Vector2 force = direction * Speed * Time.deltaTime;
            rigBody.velocity += force;
            sprite.transform.right = direction;
        }

        public override void Attack()
        {
            Vector2 direction = (attackPoint.position - transform.position).normalized;
            RaycastHit2D[] hits = Physics2D.RaycastAll(attackPoint.position, direction, 0.1f);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    Player player = hit.collider.GetComponent<Player>();
                    if (player != null)
                    {
                        player.TakeDamage(damage, pushBackStrength, direction);
                    }
                }
                
            }
        }

        protected override void Die()
        {
            CancelInvoke();

            Destroy(gameObject);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("Player"))
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                sprite.transform.right = direction;
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
