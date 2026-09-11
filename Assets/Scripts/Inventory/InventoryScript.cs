using System;
using UnityEngine;

public enum Context
{
    Overworld, Combat
}
public class InventoryScript : MonoBehaviour
{
    public static Action<InventoryScript> SerializeInventory;

    public Transform parent;
    public GameObject itemSlotPrefab;
    public bool hovering;


    public void SlotHovering(ItemSlot slot)
    {
        
    }

    public void SlotClicked(ItemSlot slot)
    {
        
    }

}