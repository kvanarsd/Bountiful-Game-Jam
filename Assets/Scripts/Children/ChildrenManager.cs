using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;
using Unity.VisualScripting;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;
using TMPro;

public class ChildrenManager : MonoBehaviour
{
    [SerializeField] private GameObject ChildPrefab;
    private List<GameObject> children = new List<GameObject>();
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    public float speedMin = 0.5f;
    public float speedMax = 2.0f;


    // other refs
    [SerializeField] private ParentManager ParentMan;
    [SerializeField] private PlayerManager PlayerMan;
    [SerializeField] private SpriteRenderer background;
    private List<Vector2> doors = new List<Vector2>();
    public TMP_Text text;

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

    // Start is called before the first frame update
    void Start()
    {
        // background dimensions
        width = 12.5f;
        streetTop = -2.5f;
        streetBottom = -4;
        doors.Add(new Vector2(-9.532f, -2.5f));
        doors.Add(new Vector2(-1.284f, -2.5f));
        doors.Add(new Vector2(0.851f, -2.5f));
        doors.Add(new Vector2(7.31f, -2.5f));

        for (int i = 0; i < maxChildren; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-width, width), Random.Range(streetBottom, streetTop), 0);
            GameObject childObj = Instantiate(ChildPrefab, randPos, Quaternion.identity, transform);
            childObj.name = "Child" + i;
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
        ChildScript script = child.GetComponent<ChildScript>();
        script.IdleCo = StartCoroutine(Idle(child));
    }

    public void StopIdle(GameObject child)
    {
        ChildScript script = child.GetComponent<ChildScript>();
        StopCoroutine(script.IdleCo);
    }

    public IEnumerator Idle(GameObject child)
    {
        timer = Random.Range(0.5f, 2f);
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
        ChildScript script = child.GetComponent<ChildScript>();
        script.WalkCo = StartCoroutine(HorWalking(child));
    }
    public void StartVertWalking(GameObject child)
    {
        ChildScript script = child.GetComponent<ChildScript>();
        script.WalkCo = StartCoroutine(VertWalking(child));
    }

    public void StopWalking(GameObject child)
    {
        ChildScript script = child.GetComponent<ChildScript>();
        StopCoroutine(script.WalkCo);
    }

    public IEnumerator HorWalking(GameObject child)
    {
        // decide how long to walk in this direction
        float timer = Random.Range(2f, 4f);
        float speed = Random.Range(speedMin, speedMax);
        ChildScript script = child.GetComponent<ChildScript>();

        while (timer > 0f)
        {
            child.transform.Translate(Vector3.right * speed * script.direction * Time.deltaTime);

            // Switch direction at boundaries
            if (child.transform.position.x >= width || child.transform.position.x <= -width)
            {
                script.direction *= -1;
            }


            timer -= Time.deltaTime;
            yield return null;
        }

        // choose new state
        string state = SelectState();
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
        float timer = Random.Range(0.25f, 1f);
        float speed = Random.Range(speedMin, speedMax);
        ChildScript script = child.GetComponent<ChildScript>();

        while (timer > 0f)
        {
            child.transform.Translate(Vector3.up * speed / 2 * script.direction * Time.deltaTime);

            // Switch direction at boundaries
            if (child.transform.position.y >= streetTop || child.transform.position.y <= streetBottom)
            {
                script.direction *= -1;
            }


            timer -= Time.deltaTime;
            yield return null;
        }

        // choose new state
        string state = SelectState();
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

    public void StartTreat(GameObject child)
    {
        ChildScript script = child.GetComponent<ChildScript>();
        script.TreatCo = StartCoroutine(TrickTreat(child));
    }
    public void StopTreat(GameObject child)
    {
        ChildScript script = child.GetComponent<ChildScript>();
        StopCoroutine(script.TreatCo);
    }

    public IEnumerator TrickTreat(GameObject child)
    {
        Vector2 door = doors[Random.Range(0, doors.Count)];
        door.x += Random.Range(-0.25f, 0.25f);

        float speed = Random.Range(speedMin, speedMax);
        float threshold = 0.01f;

        while (Vector2.Distance(child.transform.position, door) > threshold)
        {
            child.transform.position = Vector2.MoveTowards(child.transform.position, door, speed / 2 * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(1f, 3f));

        // If not treat again then move down (away from door
        string state = SelectState();
        ChildScript script = child.GetComponent<ChildScript>();

        if (state == "treat")
        {
            StartTreat(child);
        }
        else
        {
            script.vertWalking = true;
        }
    }

    public void StartFollow(GameObject child)
    {
        StartCoroutine(Follow(child));
    }
    public IEnumerator Follow(GameObject child)
    {
        ChildScript script = child.GetComponent<ChildScript>();
        Vector2 adjust = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Vector2 location = script.candy.transform.position;
        location += adjust;
        float speed = Random.Range(speedMin, speedMax);

        float threshold = 0.01f;
        while (script.following || Vector2.Distance(child.transform.position, location) > threshold)
        {
            child.transform.position = Vector2.MoveTowards(child.transform.position, location, speed/2 * Time.deltaTime);
            location = script.candy.transform.position;
            location += adjust;
            yield return null;
        }
        
    }

    public void EndFollow(GameObject child)
    {
        //called when candy exits state
        ChildScript script = child.GetComponent<ChildScript>();

        // choose new state
        string state = SelectState();
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
            script.treat = true;
        }
    }
}
