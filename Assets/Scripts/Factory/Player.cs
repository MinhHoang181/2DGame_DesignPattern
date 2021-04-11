using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Commands;
namespace DesignPattern.Factory
{
    public class Player : MonoBehaviour, Character
    {
        Command btnW, btnA, btnS, btnD;
        public int Health { get { return health; } set { health = value; } }
        public float Speed { get { return speed; } set { speed = value; } }

        [SerializeField] int health;
        [SerializeField] float speed;

        private WeaponController weapon;

        public void Awake()
        {
            btnW = new MoveForward(this);
            btnA = new MoveLeft(this);
            btnS = new MoveBack(this);
            btnD = new MoveRight(this);
        }

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
            //float inputVer = Input.GetAxis("Vertical");
            //float inputHor = Input.GetAxis("Horizontal");

            //transform.Translate(Vector3.up * inputVer * Time.deltaTime * speed);
            //transform.Translate(Vector3.right * inputHor * Time.deltaTime * speed);
            
            if (Input.GetKey(KeyCode.W))
            {
                btnW.Execute();
            }
            else if (Input.GetKey(KeyCode.A))
            {  //horizon, verticle
                btnA.Execute();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                btnS.Execute();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                btnD.Execute();
            }
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
