using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private InputActionAsset inputs;

    private PlayerState currentState = PlayerState.Overworld;

    // Input Actions
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction menuAction;
    private List<InputAction> inputActions = new List<InputAction>();


    public event Action<Vector2> MoveRequest;
    public event Action InteractionRequest;


    void OnEnable()
    {
        var actionMap = inputs.FindActionMap("OverworldActions");

        moveAction = actionMap.FindAction("Movement"); inputActions.Add(moveAction); 
        interactAction = actionMap.FindAction("Interact"); inputActions.Add(interactAction);
        menuAction = actionMap.FindAction("Menu"); inputActions.Add(menuAction);

        Enable();
    }
    void OnDisable() => Disable();

    public void Enable()
    {
        foreach(InputAction act in inputActions)
            act.Enable();
        
        interactAction.performed += OnInteract;
    }
    public void Disable()
    {
        interactAction.performed -= OnInteract;
        foreach(InputAction act in inputActions)
            act.Disable();
        
    }


    void Update()
    {
        var readValue = moveAction.ReadValue<Vector2>();
        if(readValue != Vector2.zero)
            OnMove(readValue);
        
    }

    public void CheckState()
    {
        currentState = playerController.state;
    }

    public void OnMove(Vector2 input)
    {
        CheckState();
        Vector2 movement = new Vector2(input.x, input.y);
        if(currentState == PlayerState.Overworld)
            MoveRequest?.Invoke(movement);
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        CheckState();
        if(currentState != PlayerState.Disabled && currentState != PlayerState.Combat && currentState != PlayerState.Interacting)
            InteractionRequest?.Invoke();
    }

    public void OnMenu()
    {
        // TBI
    }
}
