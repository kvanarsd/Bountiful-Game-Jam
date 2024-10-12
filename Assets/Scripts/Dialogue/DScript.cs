using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParentLines", menuName = "Parent Dialogues")]
public class DScript : ScriptableObject
{
    public DLine[] lines;  // Array of dialogue lines
}
