using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class ChildrenFSM : AbstractFiniteStateMachine
{
    public enum ChildState
    {
        IDLE,
        WALK,
        FOLLOW,
        TREAT,
        HURT
    }
    private void Awake()
    {
        Init(ChildState.IDLE,
            AbstractState.Create<IdleState, ChildState>(ChildState.IDLE, this),
            AbstractState.Create<WalkState, ChildState>(ChildState.WALK, this),
            AbstractState.Create<FollowState, ChildState>(ChildState.FOLLOW, this),
            AbstractState.Create<TreatState, ChildState>(ChildState.TREAT, this),
            AbstractState.Create<HurtState, ChildState>(ChildState.HURT, this)
        );
    }
    public class IdleState : AbstractState
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
    public class WalkState : AbstractState
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
    public class FollowState : AbstractState
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
    public class TreatState : AbstractState
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
    public class HurtState : AbstractState
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
