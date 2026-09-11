using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static DialogueParser;

public enum DialogueState {Typing, Waiting, WaitingChoice}
public class DialogueRunner : MonoBehaviour
{

    public InputActionAsset actionAsset;
    private InputAction proceed;

    public TextMeshProUGUI textDisplay;
    public float shakeIntensity;
    public TextMeshProUGUI speakerDisplay;

    public ChoiceMenu choiceMenu; 
    public DialogueGraph currentGraph;
    public Dictionary<string, DialogueNode> currentNodes;
    private DialogueState state = DialogueState.Typing;
    [SerializeField] private float talkSpeed;
    private bool isShaking;
    private Coroutine shaking;
    private Vector3 originalPos;

    public GameObject root;

    private PlayerController player;

    void OnEnable()
    {
        var map = actionAsset.FindActionMap("DialogueActions");
        proceed = map.FindAction("Select");
        proceed.Enable();
        proceed.performed += ProceedDialogue;
    }

    void OnDisable()
    {
        proceed.performed -= ProceedDialogue;
    }



    public void EndDialogue()
    {
        root.SetActive(false);
        if(player != null) player.state = PlayerState.Overworld;
    }
    public void StartDialogue(DialogueGraph g, string customNode = null)
    {
        player = FindAnyObjectByType<PlayerController>();
        if(player != null) player.state = PlayerState.Overworld;
        root.SetActive(true);
        currentGraph = g;
        currentNodes = new Dictionary<string, DialogueNode>();
        foreach(var n in g.nodes)
        {
            currentNodes[n.id] = n;
        }
        if(customNode != null)
            StartCoroutine(RunNode(customNode));
        else
            StartCoroutine(RunNode(currentGraph.startingNodeID));
    }


    private IEnumerator RunNode(string nodeID)
    {
        StopShake();
        if(!currentNodes.TryGetValue(nodeID, out DialogueNode currentNode))
        {
            state = DialogueState.Typing; yield break;
        }
        speakerDisplay.text = currentNode.speakerName;
    

        talkSpeed = currentNode.speed;

        textDisplay.text = "";
        state = DialogueState.Typing;

        List<DialogueCommand> commands = GetCommands(currentNode.text);
        foreach (var cmd in commands)
            yield return RunCommand(cmd);

        if(currentNode.choices.Count > 1 && currentNode.choices != null)
        {
            state = DialogueState.WaitingChoice;
            var chosenChoice = new ChoiceResult();
            
            yield return choiceMenu.ShowChoices(currentNode.choices, chosenChoice);
            yield return RunNode(chosenChoice.targetNodeID);
        }else
        {
            state = DialogueState.Waiting;
            yield return WaitForInput();
            if(currentNode.nextNodeID == "")
            {
                EndDialogue();
                yield break;
            }
            yield return RunNode(currentNode.nextNodeID);
        }
    }    



    private IEnumerator RunCommand(DialogueCommand cmd)
    {
        var target = textDisplay;
        switch (cmd.commandType)
        {
            case DialogueCommandType.Color:
                var str = cmd.stringInput;
                if(str == "nil")
                    textDisplay.text += $"</color>";
                else
                    textDisplay.text += $"<color={cmd.stringInput}>";
            break;
            case DialogueCommandType.Text:
                yield return TypeText(cmd.stringInput);
            break;
            case DialogueCommandType.Wait:
                yield return TimeCache.Wait(cmd.floatInput);
            break;
            case DialogueCommandType.Speed:
                talkSpeed = cmd.floatInput;
            break;
            case DialogueCommandType.Shake:
                if(cmd.stringInput == "end") StopShake();
                else 
                {
                    if (cmd.floatInput != 0) shakeIntensity = cmd.floatInput;
                    StartShake();
                }
            break;
            case DialogueCommandType.NextPage:
                state = DialogueState.Waiting;
                yield return WaitForInput();

                textDisplay.text = "";
                state = DialogueState.Typing;
            break;

        }
        yield break;
    }

    private IEnumerator TypeText(string s)
    {
        int i=0;
        
        while(i < s.Length)
        {
            char c = s[i];
            textDisplay.text += c.ToString();
            i++;
            yield return TimeCache.Wait(1f / talkSpeed);
        }
        yield break;
    }


    private void StopShake()
    {
        isShaking = false;
        if(shaking != null) StopCoroutine(shaking);
    }
    private void StartShake()
    {
        isShaking = true;
        shaking = StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        Vector3 latestOffset = new Vector3(0, 0, 0);

        while (isShaking)
        {
            Vector3 basePos = textDisplay.transform.position - latestOffset;
            Vector2 rand = UnityEngine.Random.insideUnitCircle * shakeIntensity;
            Vector3 offset = new Vector3(rand.x, rand.y, 0f);

            textDisplay.transform.position = basePos+offset;
            latestOffset = offset;
            yield return TimeCache.Wait(1f / 20f);
        }
        textDisplay.transform.position -= latestOffset;
    }

    public void ProceedDialogue(InputAction.CallbackContext context)
    {
        if(state != DialogueState.Waiting) return;
        Debug.Log("proceeding dialogue!");
        state = DialogueState.Typing;
    }

    public IEnumerator WaitForInput()
    {
        while(state == DialogueState.Waiting) yield return null;
        while(state != DialogueState.Typing) yield return null;
    }

    public void SetState(DialogueState s)
    {
        state = s;
    }


}