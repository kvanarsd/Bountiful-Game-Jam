using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class PlayerFSM : AbstractFiniteStateMachine
{
    public enum PlayerState
    {
        IDLE,
        WALK,
        KICK,
        GIVE,
        THROW
    }
    private void Awake()
    {
        Init(PlayerState.IDLE,
            AbstractState.Create<IdleState, PlayerState>(PlayerState.IDLE, this),
            AbstractState.Create<WalkState, PlayerState>(PlayerState.WALK, this),
            AbstractState.Create<KickState, PlayerState>(PlayerState.KICK, this),
            AbstractState.Create<GiveState, PlayerState>(PlayerState.GIVE, this),
            AbstractState.Create<ThrowState, PlayerState>(PlayerState.THROW, this)
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
    public class KickState : AbstractState
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
    public class GiveState : AbstractState
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
    public class ThrowState : AbstractState
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
