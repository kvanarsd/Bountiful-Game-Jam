using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
using Unity.VisualScripting;

public class ChildrenManager : MonoBehaviour
{
    public GameObject ChildPrefab;
    private List<GameObject> children = new List<GameObject>();

    public float speed = 2.0f;
    private int direction = 1;

    // other refs
    [SerializeField] private ParentManager ParentMan;
    [SerializeField] private PlayerManager PlayerMan;
    [SerializeField] private SpriteRenderer background;

    // random timer for states
    private float timer;

    public float maxChildren;

    // bounding screen
    [SerializeField] private float width;
    [SerializeField] private float streetTop;
    [SerializeField] private float streetBottom;

    // random select state
    [SerializeField] private List<string> states;
    [SerializeField] private List<float> weights;

    // variables to store coroutines
    private Coroutine IdleCo;
    private Coroutine WalkCo;
    private Coroutine TreatCo;
    private Coroutine FollowCo;

    // Start is called before the first frame update
    void Start()
    {
        // background dimensions
        width = background.bounds.size.x;
        streetTop = background.bounds.size.y / 3;
        streetBottom = 0;

        for (int i = 0; i < maxChildren; i++)
        {
            GameObject childObj = Instantiate(ChildPrefab);
            //ChildrenFSM childrenFSM =childObj.GetComponent<ChildrenFSM>();
            children.Add(childObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string SelectState()
    {
        float randomNum = Random.Range(0f, SumOfWeights());

        float cumWeight = 0f;
        for (int i = 0; i < states.Count; i++)
        {
            cumWeight += weights[i];
            if (randomNum <= cumWeight)
            {
                return states[i];
            }
        }

        // in case
        return states[states.Count - 1];
    }

    private float SumOfWeights()
    {
        float sum = 0f;
        foreach (float weight in weights)
        {
            sum += weight;
        }
        return sum;
    }

    public void StartIdle(GameObject child)
    {
        IdleCo = StartCoroutine(Idle(child));
    }

    public IEnumerator Idle(GameObject child)
    {
        timer = Random.Range(1f, 3f);
        yield return new WaitForSeconds(timer);
        ChildScript script = child.GetComponent<ChildScript>();
        
        script.idle = false;

        string state = SelectState();
        
        if (state == "horWalking")
        {
            script.horWalking = true;
        }
        else if (state == "treat")
        {
            script.treat = true;
        }
        else if (state == "vertWalking")
        {
            script.vertWalking = true;
        }
        else
        {
            StartIdle(child);
        }
    }

    public void StartHorWalking(GameObject child)
    { 
        WalkCo = StartCoroutine(HorWalking(child));
    }
    public void StartVertWalking(GameObject child)
    {
        WalkCo = StartCoroutine(VertWalking(child));
    }

    public IEnumerator HorWalking(GameObject child)
    {
        // decide how long to walk in this direction
        float timer = Random.Range(2f, 7f);

        while (timer > 0f)
        {
            child.transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

            // Switch direction at boundaries
            if (transform.position.x >= width || transform.position.x <= 0)
            {
                direction *= -1;
            }


            timer -= Time.deltaTime;
            yield return null;
        }


        // choose new state
        string state = SelectState();
        ChildScript script = child.GetComponent<ChildScript>();
        if (state == "idle") { 
            script.idle = true;
        }
        else if (state == "treat")
        {
            script.treat = true;
        }
        else if (state == "vertWalking") {
            script.vertWalking = true;
        } else { 
            StartHorWalking(child); 
        }
    }

    public IEnumerator VertWalking(GameObject child)
    {
        // decide how long to walk in this direction
        float timer = Random.Range(0.5f, 2f);

        while (timer > 0f)
        {
            child.transform.Translate(Vector3.up * speed/2 * direction * Time.deltaTime);

            // Switch direction at boundaries
            if (transform.position.y <= streetTop || transform.position.y >= streetBottom)
            {
                direction *= -1;
            }


            timer -= Time.deltaTime;
            yield return null;
        }


        // choose new state
        string state = SelectState();
        ChildScript script = child.GetComponent<ChildScript>();
        if (state == "idle")
        {
            script.idle = true;
        }
        else if (state == "treat")
        {
            script.treat = true;
        }
        else if (state == "horWalking")
        {
            script.horWalking = true;
        }
        else
        {
            StartVertWalking(child);
        }
    }
}
