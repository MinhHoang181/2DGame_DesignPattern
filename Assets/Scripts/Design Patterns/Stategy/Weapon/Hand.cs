using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Strategy
{
    public class Hand : MonoBehaviour, IWeapon
    {
        public int Damage { get { return hand.damage; } }
        public Transform ShootPoint { get { return shootPoint; } set { shootPoint = value; } }
        public ScriptableWeapon Weapon { get { return hand; } set { hand = value; } }

        private Transform shootPoint;
        private ScriptableWeapon hand;

        private float timeToFire = 0;

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
                timeToFire = hand.fireDelay;
                StartShoot(direction);
            }
        }

        private void StartShoot(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, hand.distance, GameController.Instance.HitLayers);
            if (hit)
            {
                Character character = hit.collider.GetComponent<Character>();
                if (character != null)
                {
                    character.TakeDamage(hand.damage, hand.pushBackStrength, direction);
                }
            }
        }
    }
}

