using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public enum PlayerState
{
    Interacting, 
    Overworld, 
    Combat, 
    Menu,
    Disabled
}

public class PlayerController : MonoBehaviour
{

    public Vector2 facingDirection = Vector2.right; 

    private PlayerRuntimeData playerRuntimeData;

    public PlayerRuntimeData GetRuntimeData()
    {
        return playerRuntimeData;
    }

    public PlayerState state;


    void Start()
    {
        state = PlayerState.Overworld;
    }

    void Update()
    {

    }
}
