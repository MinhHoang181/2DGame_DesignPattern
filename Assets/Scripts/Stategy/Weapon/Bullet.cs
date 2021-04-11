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

        [SerializeField] float speed = 5;
        [SerializeField] int damage = 1;

        private GameObject weapon;

        public void Shoot()
        {
            Vector3 initialPosition = new Vector3(transform.position.x, transform.position.y, 0);
            GameObject bullet = Instantiate(Weapon);
            bullet.transform.position = initialPosition;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f) * speed;
        }
    }
}
