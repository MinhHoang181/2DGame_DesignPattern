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

        [SerializeField] float speed = 5;
        [SerializeField] int damage = 1;

        private GameObject weapon;
        private Player player;

        public void Shoot()
        {
            Vector3 initialPosition = new Vector3(transform.position.x, transform.position.y, 0);
            GameObject bullet = Instantiate(Weapon);
            bullet.transform.position = initialPosition;

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
