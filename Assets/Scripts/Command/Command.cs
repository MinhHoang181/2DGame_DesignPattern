using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    [SerializeField] float speed;
    protected float moveDistance = 0.05f;
    public abstract void Execute(Transform trans);
    public virtual void Move(Transform trans) { }

    //public virtual void changeWeapon(Transform trans){}

}
