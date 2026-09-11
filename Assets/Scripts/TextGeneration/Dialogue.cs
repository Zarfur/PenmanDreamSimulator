
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueGraph
{
    public string startingNodeID;
    public List<DialogueNode> nodes;
    
}

[System.Serializable]
public class DialogueNode
{
    public string id;
    public string speakerName;
    [TextArea(3, 7)] public string text;
    public float speed = 20f;
    public string nextNodeID;
    public List<DialogueChoice> choices;
}