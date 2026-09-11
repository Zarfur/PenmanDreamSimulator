using System;
using UnityEngine;

public class PlayerRuntimeData
{

    
    /** 
        holds maxHP
    */
    public PlayerData playerData;

    public InventoryData inventoryData;
    public int currentHP;
    public float movementSpeed;


    public void changeHP(int amount)
    {
        currentHP = Math.Clamp(currentHP - amount, 0, playerData.getMaxHP());
        
    }
}