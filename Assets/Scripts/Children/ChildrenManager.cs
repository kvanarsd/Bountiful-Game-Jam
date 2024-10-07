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

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
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
}
