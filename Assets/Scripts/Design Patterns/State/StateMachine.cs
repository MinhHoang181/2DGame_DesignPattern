using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern.State
{
    public class StateMachine
    {
        #region TRANSITION CLASS
        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }
        #endregion

        private IState currentState;
        private Dictionary<Type, List<Transition>> transitionsDictionary = new Dictionary<Type, List<Transition>>();
        private List<Transition> currentTransitions = new List<Transition>();
        private List<Transition> anyTransitions = new List<Transition>();
        private static List<Transition> EmptyTransition = new List<Transition>(capacity: 0);

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.To);
            }
            currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == currentState) return;

            currentState?.OnExit();
            currentState = state;

            transitionsDictionary.TryGetValue(currentState.GetType(), out currentTransitions);
            if (currentTransitions == null)
            {
                currentTransitions = EmptyTransition;
            }

            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (transitionsDictionary.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                transitionsDictionary[from.GetType()] = transitions;
            }
            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (var trasition in anyTransitions)
            {
                if (trasition.Condition()) return trasition;
            }

            foreach (var transition in currentTransitions)
            {
                if (transition.Condition()) return transition;
            }

            return null;
        }
    }
}

