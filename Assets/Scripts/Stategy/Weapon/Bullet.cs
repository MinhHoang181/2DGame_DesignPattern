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
        private Player player;
        private Transform shootPoint;

        public void Start()
        {
            weapon = (GameObject) Resources.Load("Prefabs/Bullet", typeof(GameObject));
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        public void Shoot()
        {
            GameObject bullet = Instantiate(Weapon);
            bullet.transform.position = shootPoint.position;

            int x = 0;
            int y = 0;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * speed;
        }
    }
}
