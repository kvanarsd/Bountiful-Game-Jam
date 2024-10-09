using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
public class PlayerFSM : AbstractFiniteStateMachine
{

    
    public PlayerManager PlayMan { get; set; }

    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    float speedX, speedY;
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

        PlayMan = transform.GetComponent<PlayerManager>();
        rb = PlayMan.Player.GetComponent<Rigidbody2D>();

    }
    public class IdleState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {

            if (GetStateMachine<PlayerFSM>().PlayMan.walking)
            {
                TransitionToState(PlayerState.WALK);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.kicking)
            {
                TransitionToState(PlayerState.KICK);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.throwing)
            {
                TransitionToState(PlayerState.THROW);
            }
        }
        public override void OnExit()
        {
            GetStateMachine<PlayerFSM>().PlayMan.idle = false;
        }
    }
    public class WalkState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {
            GetStateMachine<PlayerFSM>().speedX = Input.GetAxisRaw("Horizontal") * GetStateMachine<PlayerFSM>().moveSpeed;
            GetStateMachine<PlayerFSM>().speedY = Input.GetAxisRaw("Vertical") * GetStateMachine<PlayerFSM>().moveSpeed;
            GetStateMachine<PlayerFSM>().rb.velocity = new Vector2(GetStateMachine<PlayerFSM>().speedX, GetStateMachine<PlayerFSM>().speedY).normalized * GetStateMachine<PlayerFSM>().moveSpeed;

            if (GetStateMachine<PlayerFSM>().PlayMan.idle)
            {
                TransitionToState(PlayerState.IDLE);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.kicking)
            {
                TransitionToState(PlayerState.KICK);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.throwing)
            {
                TransitionToState(PlayerState.THROW);
            }
        }
        public override void OnExit()
        {
            GetStateMachine<PlayerFSM>().PlayMan.idle = true;
            GetStateMachine<PlayerFSM>().rb.velocity = Vector2.zero;
            GetStateMachine<PlayerFSM>().PlayMan.walking = false;
        }
    }
    public class KickState : AbstractState
    {
        public override void OnEnter()
        {
        }
        public override void OnUpdate()
        {

            if (GetStateMachine<PlayerFSM>().PlayMan.idle)
            {
                TransitionToState(PlayerState.IDLE);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.walking)
            {
                TransitionToState(PlayerState.WALK);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.throwing)
            {
                TransitionToState(PlayerState.THROW);
            }
        }
        public override void OnExit()
        {

            GetStateMachine<PlayerFSM>().PlayMan.idle = true;
            GetStateMachine<PlayerFSM>().PlayMan.kicking = false;
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

            if (GetStateMachine<PlayerFSM>().PlayMan.idle)
            {
                TransitionToState(PlayerState.IDLE);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.walking)
            {
                TransitionToState(PlayerState.WALK);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.kicking)
            {
                TransitionToState(PlayerState.KICK);
            }
        }
        public override void OnExit()
        {
            GetStateMachine<PlayerFSM>().PlayMan.idle = true;
            GetStateMachine<PlayerFSM>().PlayMan.throwing = false;
        }
    }
}
