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

        // public void OnHit();

        //public void Dead();
    }
}
