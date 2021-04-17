using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;

namespace DesignPattern.Strategy
{
    public class Piston : MonoBehaviour, IWeapon
    {
        public int Damage { get { return damage; } set { damage = value; } }
        public Transform ShootPoint { get { return shootPoint; } set { shootPoint = value; } }
        public GameObject Weapon { get { return weapon; } }

        [SerializeField] int damage = 1;
        [SerializeField] float distance = 10;

        private Player player;
        private Transform shootPoint;
        private GameObject weapon;

        public void Start()
        {
            weapon = (GameObject)Resources.Load("Prefabs/Bullet Line", typeof(GameObject));
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        public void Shoot()
        {
            LineRenderer line = Instantiate(Weapon).GetComponent<LineRenderer>();
            line.SetPosition(0, shootPoint.position);
            float x = shootPoint.position.x;
            float y = shootPoint.position.y;
            switch (player.Facing)
            {
                case Direction.RIGHT:
                    x += distance;
                    break;
                case Direction.LEFT:
                    x -= distance;
                    break;
                case Direction.UP:
                    y += distance;
                    break;
                case Direction.DOWN:
                    y -= distance;
                    break;
                default:
                    break;
            }
            line.SetPosition(1, new Vector3(x, y, 0));
        }
    }
}
