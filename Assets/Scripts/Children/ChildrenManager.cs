using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KevinCastejon.FiniteStateMachine;

public class ChildrenManager : MonoBehaviour
{
    public GameObject ChildPrefab;
    private List<ChildrenFSM> children = new List<ChildrenFSM>();

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

    public IEnumerator Walking()
    {

        // decide how long to walk in this direction
        timer = Random.Range(2f, 7f);

        // end of timer stop walking
        yield return new WaitForSeconds(timer);
  
      
        // choose new state
        string state = SelectState();
        if (state == "idle") { idle = true; aud.Stop(); } else if (state == "sleep") { sleeping = true; aud.Stop(); } else { StartWalking(); }
    }
}
