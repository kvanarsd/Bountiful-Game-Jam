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
    [SerializeField] private Camera cam;

    public bool idle = true;
    public bool walking = false;
    public bool kicking = false;
    public bool throwing = false;

    //shooting stuff
    public GameObject CandyPrefab;
    public Transform offset;
    public float candySpeed = 20f;

    [SerializeField] private Sprite candy1;
    [SerializeField] private Sprite candy2;
    [SerializeField] private Sprite candy3;


    //animations



    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            if (Input.anyKey == false)
            {
                idle = true;
            }



            if (throwing == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("throw click");
                    idle = false;
                    throwing = true;
                    Debug.Log("throw true " + throwing);
                    Throw();
                }
            }

            if (kicking == false && throwing == false)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                {
                    walking = true;
                    idle = false;

                }
                else
                {
                    idle = true;
                    walking = false;
                }
            }
        }
        

        

    }

    public void Throw()
    {
        Debug.Log("throwing");
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("mouse");
        Vector3 throwDir = (mousePos - Player.transform.position).normalized;
        Debug.Log("dir");
        GameObject candy = Instantiate(CandyPrefab, Player.transform.position, Quaternion.identity);
        Debug.Log("instan");

        //candy.tag = "Candy";

        Rigidbody2D cRB = candy.GetComponent<Rigidbody2D>();
        Debug.Log("rigi");

        cRB.velocity = new Vector2(throwDir.x, throwDir.y) * candySpeed;
        Debug.Log("velocity");

        Vector3 targetPos = new Vector3(mousePos.x, mousePos.y, 0);

        candy.AddComponent<CandyBehavior>().Initialize(cRB, targetPos);

        idle = true;
        throwing = false;

        /*if (Player.transform.localRotation.y == 0)
        {
            cRB.velocity = new Vector2(throwDir.x, throwDir.y) * candySpeed;
        }
        else
        {
            cRB.velocity = new Vector2(throwDir.x, throwDir.y) * -candySpeed;
        }*/


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(idle == true)
        {
            Player.anim.Play("MC_idle");
            if (collision.tag == "HealthyDoor" || collision.tag == "ChocolateDoor" || collision.tag == "SweetDoor" || collision.tag == "SourDoor")
            {
                Player.anim.Play("MC_love");
            }
        }
    }

}
