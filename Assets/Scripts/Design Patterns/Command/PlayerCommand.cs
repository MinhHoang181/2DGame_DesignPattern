using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.Commands
{
    public abstract class PlayerCommand : Command
    {
        public Player player;
    }
}

