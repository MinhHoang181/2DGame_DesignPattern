using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Factory
{
    public class Player : MonoBehaviour, Character
    {
        public int Health { get { return health; } set { health = value; } }
        public float Speed { get { return speed; } set { speed = value; } }

        [SerializeField] int health;
        [SerializeField] float speed;

        private WeaponController weapon;

        public void Start()
        {
            weapon = transform.GetComponent<WeaponController>();
        }

        public void Update()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack(); // Tu lam
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Press C");
                ChangeWeapon(); // Tu lam
            }
        }

        public void Move()
        {
            float inputVer = Input.GetAxis("Vertical");
            float inputHor = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.up * inputVer * Time.deltaTime * speed);
            transform.Translate(Vector3.right * inputHor * Time.deltaTime * speed);

            //MoveVer(speed, transform);
            //MoveHor();
        }

        public void Attack() // 
        {
            //Debug.Log("Ban");
            weapon.Fire();
        }

        public void ChangeWeapon() // Dinh nghia
        {
            weapon.Weapon(WeaponType.Bullet);
        }
    }
}
