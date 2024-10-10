using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class ParentManager : MonoBehaviour
{
    public GameObject ParentPrefab;
    private List<ParentsFSM> parents = new List<ParentsFSM>();

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
