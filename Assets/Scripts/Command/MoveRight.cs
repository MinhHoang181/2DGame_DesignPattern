using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : Command
{
    public override void Execute(Transform trans)
    {
        Move(trans);
    }

    public override void Move(Transform trans)
    {
        trans.Translate(trans.right * moveDistance );
    }
}