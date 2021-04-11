using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Strategy
{
    public class Shooting : MonoBehaviour
    {
        public Transform firePoint;
        public Transform firePoint1;
        public Transform firePoint2;
        public GameObject bulletPrefab;

        public float bulletForce = 20f;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                
                Shoot1Tia();
            }
        }
        void Shoot1Tia()
        {

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode2D.Impulse);
  
        }

        void Shoot2Tia()
        {
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();

            rb1.AddForce(firePoint1.forward * bulletForce, ForceMode2D.Impulse);

            GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();

            rb2.AddForce(firePoint2.forward * bulletForce, ForceMode2D.Impulse);
        }
    }
}

