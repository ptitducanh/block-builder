using System.Collections;
using System.Collections.Generic;
using BuilderSystem;
using Data;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private BlockData blockData;
    
    private Dictionary<long, InventoryItemUI> _inventoryItemUIs = new();
    private IInventory                        _inventory;
    private IBuilder                          _builder;
    
    // Start is called before the first frame update
    void Awake()
    {
        _inventory = ServiceLocator.Get<IInventoryService>().GetPlayerInventory();
        _builder   = ServiceLocator.Get<IBuilder>();
        
        _builder.OnPlayerPlacedBlock    += OnPlayerPlacedBlock;
        _builder.OnPlayerRetrievedBlock += OnPlayerRetrievedBlock;
        
        SetupUIVisual();
    }

    #region private functions

    private void SetupUIVisual()
    {
        var items = _inventory.GetAllItems<IItem>();
        for (int i = 0; i < items.Length; i++)
        {
            var item   = items[i];
            var itemUI = Instantiate(inventoryItemPrefab, transform).GetComponent<InventoryItemUI>();
            itemUI.gameObject.SetActive(true);
            itemUI.PopulateItem(this, item.Id, blockData.GetBlockItem(item.Id).Icon, _inventory.GetItemQuantity(item.Id));
            _inventoryItemUIs.Add(item.Id, itemUI);
        }
    }

    #endregion

    #region event handlers
    /// <summary>When the player placed a block, we need to update the quantity of the block in the inventory.</summary>
    private void OnPlayerPlacedBlock(Block block)
    {
        _inventory.GetItem<IItem>(block.Id);
        _inventoryItemUIs[block.Id].SetQuantity(_inventory.GetItemQuantity(block.Id));
    }
    
    /// <summary>When the player retrieved a block, we need to update the quantity of the block in the inventory.</summary>
    private void OnPlayerRetrievedBlock(Block block)
    {
        _inventory.AddItem<IItem>(new BlockItem() { Id = block.Id });
        _inventoryItemUIs[block.Id].SetQuantity(_inventory.GetItemQuantity(block.Id));
    }

    public void OnPlayerClickItem(long itemId)
    {
        _builder.SelectBlock(itemId);
    }
    #endregion
}
