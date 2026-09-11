using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]

public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
}