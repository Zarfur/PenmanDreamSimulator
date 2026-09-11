using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]

public class DialogueChoice
{
    public string targetNodeID;
    public string choiceText;
}


public class ChoiceMenu : MonoBehaviour
{
    public GameObject choiceParent;

    private bool chosen;
    private List<DialogueChoice> choices;
    public DialogueRunner dialogueRunner;
    public List<GameObject> choiceLabels;    
    private int chosenIndex;
    private int highlightedIndex;



    public IEnumerator ShowChoices(List<DialogueChoice> choices, ChoiceResult result)
    {
        this.choices = choices;
        chosenIndex = 0;
        chosenIndex = -1;
        choiceParent.SetActive(true);

        highlightedIndex = 0;
        ShowHighlightectOption();

        Refresh();
        chosen = false;
        while (!chosen)
        {
            yield return null;
        }


        choiceParent.SetActive(false);
        while(chosenIndex == -1)
            yield return null;
        
        result.targetNodeID = this.choices[chosenIndex].targetNodeID;


        Refresh();


        yield break;
    }



    private void Refresh()
    {
        for(int i=0; i < choiceLabels.Count; i++)
        {
            bool active = i < choices.Count;
            choiceLabels[i].SetActive(active);
            if(active) {
                choiceLabels[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].choiceText;
                var button = choiceLabels[i].GetComponent<Button>();
                button.onClick.RemoveAllListeners();


                int index = i;
                button.onClick.AddListener(() => OnClick(index));
            }
        }
    }

    public void OnClick(int i)
    {
        chosen = true;
        dialogueRunner.SetState(DialogueState.Typing);
        chosenIndex = i;
        choiceParent.SetActive(false);

    }


}
public class ChoiceResult
{
    public string targetNodeID;
}