using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class ParentsFSM : AbstractFiniteStateMachine
{
    public enum ParentState
    {
        CLOSED,
        OPEN,
        WATCH,
        CANDY,
        ANGRY
    }
    private void Awake()
    {
        Init(ParentState.CLOSED,
            AbstractState.Create<ClosedState, ParentState>(ParentState.CLOSED, this),
            AbstractState.Create<ClosedState, ParentState>(ParentState.OPEN, this),
            AbstractState.Create<WatchState, ParentState>(ParentState.WATCH, this),
            AbstractState.Create<CandyState, ParentState>(ParentState.CANDY, this),
            AbstractState.Create<AngryState, ParentState>(ParentState.ANGRY, this)
        );
    }
    public class ClosedState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
    public class OpenState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
    public class WatchState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
    public class CandyState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
    public class AngryState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
}
