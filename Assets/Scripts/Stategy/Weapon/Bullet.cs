using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Strategy
{
    public class Bullet : MonoBehaviour, IWeapon
    {
        public float Speed { get { return speed; } set { speed = value; } }
        public int Damage { get { return damage; } set { damage = value; } }
        public GameObject Weapon { get { return weapon; } set { weapon = value; } }
        public Player Player { get { return player; } set { player = value; } }
        public Transform ShootPoint { get { return shootPoint; } set { shootPoint = value; } }

        [SerializeField] float speed = 20;
        [SerializeField] int damage = 1;

        private GameObject weapon;
        private Player player;
        private Transform shootPoint;

        public void Shoot()
        {
            GameObject bullet = Instantiate(Weapon);
            bullet.transform.position = shootPoint.position;

            int x = 0;
            int y = 0;
            switch (Player.Facing)
            {
                case Direction.RIGHT:
                    x = 1;
                    break;
                case Direction.LEFT:
                    x = -1;
                    break;
                case Direction.UP:
                    y = 1;
                    break;
                case Direction.DOWN:
                    y = -1;
                    break;
                default:
                    break;
            }

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * speed;
        }
    }
}
