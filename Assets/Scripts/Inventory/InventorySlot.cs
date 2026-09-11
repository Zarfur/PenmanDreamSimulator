using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ItemSlot slot;
    public TextMeshProUGUI text;
    public Button button;

    private InventoryScript inventory;

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventory.hovering = true;
        inventory.SlotHovering(slot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventory.hovering = false;
        inventory.SlotHovering(slot);
    }

    public void Set(ItemSlot slot, InventoryScript inventory)
    {
        this.slot = slot;
        this.inventory = inventory;
        
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        Load();
    }


    private void Load()
    {
        bool isEmpty = slot == null || slot.itemData == null || slot.stacks <= 0;
        if (isEmpty)
        {
            text.enabled = false; return;
        }

        text.text = $"x{slot.stacks} {slot.itemData.itemName}";
        
        text.enabled = true;
    }


    private void OnClick()
    {
        if (slot == null || slot.itemData == null) return;
        inventory.SlotClicked(slot);
    }
}