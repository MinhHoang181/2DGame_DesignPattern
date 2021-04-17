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

        [SerializeField] int damage = 1;

        private Player player;
        private Transform shootPoint;

        public void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        public void Shoot()
        {
            
        }
    }
}
