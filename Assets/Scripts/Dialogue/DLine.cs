using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DLine
{
    [TextArea] public string dialogueText; //text
    public Sprite parentEmote; //associated sprite
    public DChoice[] choices; //choices, usually array length 2
}
