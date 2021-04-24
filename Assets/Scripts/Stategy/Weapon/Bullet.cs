using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Strategy
{
    public class Bullet : MonoBehaviour, IWeapon
    {
        public int Damage { get { return damage; } set { damage = value; } }
        public int PushBackStrength { get { return pushBackStrength; } set { pushBackStrength = value; } }
        public Transform ShootPoint { get { return shootPoint; } set { shootPoint = value; } }
        public GameObject Weapon { get { return weapon; } }

        [SerializeField] float speed = 20;
        [SerializeField] int damage = 1;
        [SerializeField] int pushBackStrength = 100;

        private GameObject weapon;
        private Transform shootPoint;

        public void Start()
        {
            weapon = (GameObject) Resources.Load("Prefabs/Bullet", typeof(GameObject));
        }

        public void Shoot(Vector2 direction)
        {
            GameObject bullet = Instantiate(Weapon);
            bullet.transform.position = shootPoint.position;

            bullet.GetComponent<Rigidbody2D>().velocity = direction * speed * Time.deltaTime;
        }
    }
}
