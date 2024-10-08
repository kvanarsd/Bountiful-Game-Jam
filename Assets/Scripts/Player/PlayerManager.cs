using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class PlayerManager : MonoBehaviour
{
    public PlayerScript Player;
    [SerializeField] private PlayerFSM PlayerSM;

    private Rigidbody2D rb;

    // other refs
    [SerializeField] private ParentManager ParentMan;
    [SerializeField] private ChildrenManager ChildMan;

    public bool idle = true;
    public bool walking = false;
    public bool kicking = false;
    public bool throwing = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKey == false)
        {
            idle = true;
        }

    }
}
