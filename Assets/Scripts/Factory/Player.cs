using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Strategy;
using TMPro;
using System;

namespace DesignPattern.Factory
{
    public class Player : Character
    {
        public WeaponController Weapon { get { return weapon; } }

        private ScriptablePlayer scriptablePlayer;
        private WeaponController weapon;
        
        private Vector3 directionMove;

        protected override void Start()
        {
            base.Start();

            weapon = transform.GetComponent<WeaponController>();

            Setting();
        }

        private void Update()
        {
            Action();
        }

        protected override void Setting()
        {
            base.Setting();

            scriptablePlayer = (ScriptablePlayer)scriptableCharacter;
            // WeaponController
            weapon.AddWeapon(scriptablePlayer.weapon);
        }

        private void Action()
        {
            if (isTakeDamage) return;
            FacingToMouse();
        }

        public override void Move(Vector3 direction)
        {
            if (isTakeDamage) return;

            directionMove = (directionMove + direction).normalized;
            //Debug.Log(directionMove);
            transform.Translate(direction * Speed * Time.deltaTime);
        }

        public override void Attack()
        {
            if (isTakeDamage) return;

            Vector2 direction = (attackPoint.position - transform.position).normalized;
            weapon.Fire(direction);
        }

        protected override void Die()
        {
            Debug.Log("Game over");
        }

        private void FacingToMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );

            sprite.transform.right = direction;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.tag.Equals("Zombie"))
            {
                if (isTakeDamage == false)
                {
                    rigBody.velocity = Vector2.zero;
                }
            }
        }
    }
}
