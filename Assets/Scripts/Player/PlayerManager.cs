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

           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                idle = false;
                kicking = true;
                StartCoroutine(Kick());
            }
            
            

            if (kicking == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("throw click");
                    idle = false;
                    throwing = true;
                    Debug.Log("throw true " + throwing);
                    Throw();
                }

                if(throwing == false)
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

        int ranCandy = Random.Range(1, 4);
        if (ranCandy == 1)
        {
            candy.GetComponent<SpriteRenderer>().sprite = candy1;
        }else if (ranCandy == 2)
        {
            candy.GetComponent<SpriteRenderer>().sprite = candy2;
        }else if (ranCandy == 3)
        {
            candy.GetComponent<SpriteRenderer>().sprite = candy3;
        }

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

    private IEnumerator Kick()
    {
        yield return new WaitForSeconds(1f);
        idle = true;
        kicking = false;
    }



}
