using System;
using System.Collections;
using UnityEngine;


public class DialogueController : MonoBehaviour
{
    private DialogueRunner dialogueRunner;



    void OnEnable()
    {
        var overworldRoot = GameObject.Find("OverworldRoot");
        dialogueRunner = overworldRoot.GetComponentInChildren<DialogueRunner>();

        DontDestroyOnLoad(gameObject);

        // PLACEHOLDER
        DialogueGraph graph = FileLoader.LoadDialogueGraph("Text/TestWords");
        StartCoroutine(TestDialogueMethod(graph));
    }

    IEnumerator TestDialogueMethod(DialogueGraph g)
    {
        yield return TimeCache.Wait(5f);
        
        //dialogueRunner.StartDialogue(g);
    }
}