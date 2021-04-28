using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Strategy
{
    public class Piston : MonoBehaviour, IWeapon
    {
        public int Damage { get { return piston.damage; } }
        public Transform ShootPoint { get { return shootPoint; } set { shootPoint = value; } }
        public ScriptableWeapon Weapon { get { return piston; } set { piston = (ScriptablePiston)value; } }

        private float timeToFire = 0;

        private Transform shootPoint;
        private ScriptablePiston piston;

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
                timeToFire = piston.fireDelay;
                StartShoot(direction);
            }
        }

        private void StartShoot(Vector2 direction)
        {
            LineRenderer line = Instantiate(piston.bullet).GetComponent<LineRenderer>();

            Vector3 endLine = shootPoint.position + new Vector3(direction.x, direction.y, 0) * piston.distance;

            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, piston.distance, GameController.Instance.HitLayers);
            if (hit)
            {
                Character character = hit.collider.GetComponent<Character>();
                if (character != null)
                {
                    character.TakeDamage(piston.damage, piston.pushBackStrength, direction);
                }
                endLine = hit.point;
            }

            line.SetPosition(0, shootPoint.position);
            line.SetPosition(1, endLine);
        }
    }
}
