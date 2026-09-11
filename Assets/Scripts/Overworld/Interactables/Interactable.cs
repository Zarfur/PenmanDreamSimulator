
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Interactable : MonoBehaviour
{
    private DialogueGraph currentContext;

    void OnEnable()
    {
        FileLoader.LoadContexts();
    }
    private IEnumerator LoadCurrentContext(string given)
    {
        Debug.Log("loading current context...");
        if(FileLoader.jsonFinder != null) Debug.Log("found loader");
        if(FileLoader.jsonFinder.TryGetValue(given, out var graph)){
            currentContext = FileLoader.LoadDialogueGraph(graph);
            yield break;
        }
        yield return null;
        
    }
    public IEnumerator RunInteraction(TileBase tile)
    {
        yield return StartCoroutine(LoadCurrentContext(tile.name));
        DialogueRunner dialogueRunner = GameObject.FindAnyObjectByType<DialogueRunner>();
        dialogueRunner.StartDialogue(currentContext, tile.name);
    }
}