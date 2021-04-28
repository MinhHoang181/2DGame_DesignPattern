using System.Collections;
using System.Collections.Generic;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern.State
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}

