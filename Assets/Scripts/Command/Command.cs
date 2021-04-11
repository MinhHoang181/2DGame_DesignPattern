using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern.Factory;
namespace DesignPattern.Commands

{
    public abstract class Command
    {
        //[SerializeField] float speed;
        protected float moveDistance = 0.05f;

        
 
        public abstract void Execute();

    }
}


