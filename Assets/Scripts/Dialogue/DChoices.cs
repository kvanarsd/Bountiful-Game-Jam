using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DChoice
{
    [TextArea] public string choiceText; //text
    public int hearts; //how many hearts the choice is worth
    public int nextIndex; //the index of the next dialogue line
}
