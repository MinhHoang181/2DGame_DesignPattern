using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Factory
{
    public interface Character
    {
        public ScriptableCharacter ScriptableCharacter { get; }
        public int Health { get; }
        public int CurrentHealth { get; }
        public float Speed { get; }

        public SpriteRenderer Sprite { get; }


        public void Move(Vector3 direction);

        public void Attack();

        public void Setting();

        public void TakeDamage(int damage, float pushBackStrength, Vector2 direction);

        public void Die();
    }
}
