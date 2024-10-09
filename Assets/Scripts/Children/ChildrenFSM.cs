using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class ChildrenFSM : AbstractFiniteStateMachine
{
    public ChildrenManager Manager { get; set; }
    private GameObject child;
    public enum ChildState
    {
        IDLE,
        HORWALK,
        VERTWALK,
        FOLLOW,
        TREAT,
        HURT
    }
    private void Awake()
    {
        Init(ChildState.IDLE,
            AbstractState.Create<IdleState, ChildState>(ChildState.IDLE, this),
            AbstractState.Create<HorizontalWalkState, ChildState>(ChildState.HORWALK, this),
            AbstractState.Create<VerticalWalkState, ChildState>(ChildState.VERTWALK, this),
            AbstractState.Create<FollowState, ChildState>(ChildState.FOLLOW, this),
            AbstractState.Create<TreatState, ChildState>(ChildState.TREAT, this),
            AbstractState.Create<HurtState, ChildState>(ChildState.HURT, this)
        );

        child = this.gameObject;
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
    public class HorizontalWalkState : AbstractState
    {
        public override void OnEnter()
        {
            GetStateMachine<ChildrenFSM>().Manager.StartWalking(GetStateMachine<ChildrenFSM>().child);
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
        }
    }
    public class VerticalWalkState : AbstractState
    {
        public override void OnEnter()
        {
            GetStateMachine<ChildrenFSM>().Manager.StartWalking(GetStateMachine<ChildrenFSM>().child);
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
