using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Strategy
{
    public class Piston : MonoBehaviour, IWeapon
    {
        public int Damage { get { return damage; } set { damage = value; } }
        public int PushBackStrength { get { return pushBackStrength; } set { pushBackStrength = value; } }
        public Transform ShootPoint { get { return shootPoint; } set { shootPoint = value; } }
        public GameObject Weapon { get { return weapon; } }

        [SerializeField] int damage = 1;
        [SerializeField] int pushBackStrength = 1;
        [SerializeField] float distance = 5;
        [SerializeField] float fireDelay = 0.5f;
        private float timeToFire = 0;

        private Transform shootPoint;
        private GameObject weapon;

        public void Start()
        {
            weapon = (GameObject)Resources.Load("Prefabs/Bullet Line", typeof(GameObject));
        }

        public void Update()
        {
            if (timeToFire > 0)
            {
                timeToFire -= Time.deltaTime;
            }
        }

        public void Shoot(Vector2 direction)
        {
            if (timeToFire <= 0)
            {
                timeToFire = fireDelay;
                StartShoot(direction);
            }
        }

        private void StartShoot(Vector2 direction)
        {
            LineRenderer line = Instantiate(Weapon).GetComponent<LineRenderer>();

            //Vector2 direction = (shootPoint.position - player.transform.position).normalized;

            Vector3 endLine = shootPoint.position + new Vector3(direction.x, direction.y, 0) * distance;

            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, distance);
            if (hit)
            {
                Character character = hit.collider.GetComponent<Character>();
                if (character != null)
                {
                    character.TakeDamage(damage, PushBackStrength, direction);
                }
                endLine = hit.point;
            }

            line.SetPosition(0, shootPoint.position);
            line.SetPosition(1, endLine);
        }
    }
}
