using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
using Unity.VisualScripting;
using UnityEngine.UI;

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
        width = 12.5f;
        streetTop = -2.5f;
        streetBottom = -4;
        Debug.Log(width + " " + streetTop + " " + streetBottom);

        for (int i = 0; i < maxChildren; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-width, width), Random.Range(streetBottom, streetTop), 0);
            GameObject childObj = Instantiate(ChildPrefab, randPos, Quaternion.identity, transform);
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
        timer = Random.Range(0.5f, 1.5f);
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
            if (child.transform.position.x >= width || child.transform.position.x <= -width)
            {
                Debug.Log("switch");
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
            if (child.transform.position.y >= streetTop || child.transform.position.y <= streetBottom)
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

    public void TrickTreat (GameObject child)
    {
        // choose new state
        string state = SelectState();
        ChildScript script = child.GetComponent<ChildScript>();
        if (state == "idle")
        {
            script.idle = true;
        }
        else if (state == "vertWalking")
        {
            script.vertWalking = true;
        }
        else if (state == "horWalking")
        {
            script.horWalking = true;
        }
        else
        {
            TrickTreat(child);
        }
    }
}
