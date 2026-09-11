using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class ItemSlot
{
    public ItemData itemData;
    public int stacks;
}

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    public List<ItemSlot> inventory = new List<ItemSlot>();

    public static event Action InventoryChanged;

    public void AddItem(ItemData item, int amount)
    {
        if(item == null || amount <= 0) return;

        var existing = inventory.Find(i => i.itemData == item);
        if(existing != null) existing.stacks += amount;
        else inventory.Add(new ItemSlot {itemData = item, stacks = amount});

        InventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemData item, int amount)
    {
        var existing = inventory.Find(i => i.itemData == item);
        if(existing == null) return;
        existing.stacks -= amount;
        if(existing.stacks <=0) inventory.Remove(existing);
        InventoryChanged?.Invoke();
    }
}