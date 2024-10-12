using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
using UnityEditor;
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
        THROW
    }
    private void Awake()
    {
        Init(PlayerState.IDLE,
            AbstractState.Create<IdleState, PlayerState>(PlayerState.IDLE, this),
            AbstractState.Create<WalkState, PlayerState>(PlayerState.WALK, this),
            AbstractState.Create<KickState, PlayerState>(PlayerState.KICK, this),
            AbstractState.Create<ThrowState, PlayerState>(PlayerState.THROW, this)
        );

        PlayMan = transform.GetComponent<PlayerManager>();
        rb = PlayMan.Player.GetComponent<Rigidbody2D>();

        PlayMan.Player.anim.StopPlayback();

    }
    public class IdleState : AbstractState
    {
        public override void OnEnter()
        {
            GetStateMachine<PlayerFSM>().PlayMan.Player.anim.StopPlayback();
            GetStateMachine<PlayerFSM>().PlayMan.Player.anim.Play("MC_idle");
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
            GetStateMachine<PlayerFSM>().PlayMan.Player.anim.StopPlayback();
            GetStateMachine<PlayerFSM>().PlayMan.idle = false;
        }
    }
    public class WalkState : AbstractState
    {
        public override void OnEnter()
        {
            GetStateMachine<PlayerFSM>().PlayMan.Player.anim.Play("MC_walk");
        }
        public override void OnUpdate()
        {
            GetStateMachine<PlayerFSM>().speedX = Input.GetAxisRaw("Horizontal") * GetStateMachine<PlayerFSM>().moveSpeed;
            GetStateMachine<PlayerFSM>().speedY = Input.GetAxisRaw("Vertical") * GetStateMachine<PlayerFSM>().moveSpeed;
            GetStateMachine<PlayerFSM>().rb.velocity = new Vector2(GetStateMachine<PlayerFSM>().speedX, GetStateMachine<PlayerFSM>().speedY).normalized * GetStateMachine<PlayerFSM>().moveSpeed;

            if (GetStateMachine<PlayerFSM>().speedX > 0)
            {
                GetStateMachine<PlayerFSM>().PlayMan.Player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (GetStateMachine<PlayerFSM>().speedX < 0)
            {
                GetStateMachine<PlayerFSM>().PlayMan.Player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            
            if(GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.x < -11.2)
            {
                GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position = new Vector2(-11.2f, GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.y);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.x > 11.2)
            {
                GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position = new Vector2(11.2f, GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.y);
            }

            if (GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.y > -2.088629)
            {
                GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position = new Vector2(GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.x, -2.088629f);
            }
            if (GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.y < -3.9)
            {
                GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position = new Vector2(GetStateMachine<PlayerFSM>().PlayMan.Player.transform.position.x, -3.9f);
            }

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
            GetStateMachine<PlayerFSM>().PlayMan.Player.anim.StopPlayback();
            GetStateMachine<PlayerFSM>().PlayMan.idle = true;
            GetStateMachine<PlayerFSM>().rb.velocity = Vector2.zero;
            GetStateMachine<PlayerFSM>().PlayMan.walking = false;
        }
    }
    public class KickState : AbstractState
    {
        public override void OnEnter()
        {
            GetStateMachine<PlayerFSM>().PlayMan.Player.anim.Play("MC_kick");
            GetStateMachine<PlayerFSM>().PlayMan.Player.leg.SetActive(true);
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
            GetStateMachine<PlayerFSM>().PlayMan.Player.anim.StopPlayback();
            GetStateMachine<PlayerFSM>().PlayMan.Player.leg.SetActive(false);
            GetStateMachine<PlayerFSM>().PlayMan.idle = true;
            GetStateMachine<PlayerFSM>().PlayMan.kicking = false;
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
