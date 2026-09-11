
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public static class FileLoader
{
 
    public static bool loadedContext = false;
    public static Dictionary<string, string> jsonFinder;
    public static DialogueGraph LoadDialogueGraph(string path)
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>(path);
        if(jsonAsset == null){
            Debug.Log("Errrorororo");
            return null;}
        Debug.Log("loading file from " + path);
        return JsonUtility.FromJson<DialogueGraph>(jsonAsset.text);
    }

    public static void LoadContexts()
    {
        if(loadedContext) return;
        Debug.Log("loading contexts for text...");
        TextAsset jsonAsset = Resources.Load<TextAsset>("Text/InteractionContexts");
        if (jsonAsset != null)
        {
            jsonFinder = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonAsset.text);
            loadedContext = true;
        }

    }



    
}