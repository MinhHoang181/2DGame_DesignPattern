using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Factory
{
    public interface Character
    {
        int Health { get; set; }
        float Speed { get; set; }

        public void Move();
        public void Attack();

        public void Setting();

        public void TakeDamage(int damage, float knockBackStrength, Vector2 direction);

        public void Die();
    }
}
