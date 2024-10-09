using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class ChildrenManager : MonoBehaviour
{
    public GameObject ChildPrefab;
    private List<ChildrenFSM> children = new List<ChildrenFSM>();

    public float speed = 2.0f;
    public float walkDistance = 3.0f;
    private float startPosition;
    private int direction = 1;

    // other refs
    [SerializeField] private ParentManager ParentMan;
    [SerializeField] private PlayerManager PlayerMan;

    public bool idle = true;
    public bool walking = false;
    public bool following = false;
    public bool hurt = false;
    public bool treat = false;

    // random timer for states
    private float timer;

    public float maxChildren;
    private float numChildren;

    // bounding screen
    [SerializeField] private float width;
    [SerializeField] private float screenTop;
    [SerializeField] private float screenBottom;

    // random select state
    [SerializeField] private List<string> states;
    [SerializeField] private List<float> weights;

    // Start is called before the first frame update
    void Start()
    {
        numChildren = Random.Range(0f, maxChildren);

        for (int i = 0; i < numChildren; i++)
        {
            GameObject childObj = Instantiate(ChildPrefab);
            ChildrenFSM childrenFSM =childObj.GetComponent<ChildrenFSM>();
            children.Add(childrenFSM);
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

    public IEnumerator Walking()
    {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

        // Switch direction at boundaries
        if (Mathf.Abs(transform.position.x - startPosition) >= walkDistance)
        {
            direction *= -1;
        }

        // decide how long to walk in this direction
        timer = Random.Range(2f, 7f);

        // end of timer stop walking
        yield return new WaitForSeconds(timer);
  
      
        // choose new state
        string state = SelectState();
        //if (state == "idle") { idle = true; aud.Stop(); } else if (state == "sleep") { sleeping = true; aud.Stop(); } else { StartWalking(); }
    }
}
