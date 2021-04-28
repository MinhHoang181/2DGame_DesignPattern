using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.State
{
    public class AttackState : IState
    {
        private Zombie zombie;
        private float timeWait;

        public AttackState(Zombie zombie)
        {
            this.zombie = zombie;
        }

        public void OnEnter()
        {
            timeWait = zombie.TimeToAttack;
            zombie.IsAttack = true;
        }

        public void OnExit()
        {
            zombie.IsAttack = false;
        }

        public void Tick()
        {
            if (zombie.IsAttack == false)
            {
                zombie.IsAttack = true;
                timeWait = zombie.TimeToAttack;
            } else
            {
                timeWait -= Time.deltaTime;
            }

            if (timeWait <= 0)
            {
                zombie.Attack();
                zombie.IsAttack = false;
            }
        }
    }
}

