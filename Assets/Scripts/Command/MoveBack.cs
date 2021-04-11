using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.Commands

{
    public class MoveBack : Command
    {

        public override void Execute(Transform trans)
        {
            Move(trans);
        }

        public override void Move(Transform trans)
        {
            trans.Translate(trans.up * -moveDistance);
        }
    }
}

