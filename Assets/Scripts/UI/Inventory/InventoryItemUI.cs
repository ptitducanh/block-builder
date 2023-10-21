using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image    icon;
    [SerializeField] private TMP_Text quantityText;
    
    private InventoryUI _inventoryUI;
    private long _itemId;
    
    public void PopulateItem(InventoryUI inventoryUI, long itemId, Sprite itemSprite, int quantity)
    {
        _inventoryUI      = inventoryUI;
        _itemId           = itemId;
        icon.sprite       = itemSprite;
        quantityText.text = quantity.ToString();
    }
    
    public void SetQuantity(int quantity)
    {
        quantityText.text = quantity.ToString();
    }
    
    /// <summary>If player click on a block. We will notify the inventory UI to select that block.</summary>
    public void OnClick()
    {
        // _inventoryUI.OnPlayerClickItem(_itemId);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _inventoryUI.OnPlayerClickItem(_itemId);
    }
}
