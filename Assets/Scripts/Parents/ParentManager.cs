using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentManager : MonoBehaviour
{
    public ParentScript Parent;
    [SerializeField] private ParentsFSM ParentSM;

    // other refs
    [SerializeField] private ChildrenManager ChildMan;
    [SerializeField] private PlayerManager PlayerMan;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
