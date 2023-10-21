using System;
using System.Collections;
using Data;
using UnityEngine;

/// <summary>
/// The idea here is that the InventoryService is responsible for managing all the inventories in the game.
/// Because in the future we can have more than just 1 inventory.
/// </summary>
public class InventoryService : MonoBehaviour, IInventoryService, IBootstrapLoader
{
    /// <summary>
    /// This contains all the type of blocks in the game.
    /// </summary>
    [SerializeField] private BlockData blockData;

    private ISaveLoadSystem   _saveLoadSystem;
    private IInventory        _playerInventory;
    private InventorySaveData _saveData;

    public IEnumerator Load()
    {
        // This is a hack to make sure that the InventoryService is registered in the ServiceLocator.
        ServiceLocator.Register<IInventoryService>(this);
        LoadInventoryData();
        _saveLoadSystem = ServiceLocator.Get<ISaveLoadSystem>();
        yield return null;
    }

    public IInventory GetPlayerInventory()
    {
        return _playerInventory;
    }

    /// <summary>
    /// Try to load the inventory data from the save file.
    /// If there's no save file, then create a new inventory for the player.
    /// </summary>
    private void LoadInventoryData()
    {
        var saveLoadSystem = ServiceLocator.Get<ISaveLoadSystem>();
        _playerInventory = new Inventory();

        // Try to load the inventory data from the save file.
        _saveData = saveLoadSystem.LoadData<InventorySaveData>();
        if (_saveData == null)
        {
            // If there's no save file, then create a new inventory for the player.
            _saveData       = new InventorySaveData();
            _saveData.Items = new();

            // Create a new inventory for the player.
            // Then fill it with 10 of each block.
            foreach (var blockData in blockData.Blocks)
            {
                var blockItem = new BlockItem()
                {
                    Id = blockData.Id,
                };
                _playerInventory.AddItem<BlockItem>(blockItem, 10);
                _saveData.Items.Add(new InventoryItemData()
                {
                    ItemId = blockItem.Id,
                    Amount = 10
                });
            }


            saveLoadSystem.SaveData(_saveData);
        }
        else
        {
            // fill the inventory with the data from the save file.
            foreach (var itemData in _saveData.Items)
            {
                var blockItem = new BlockItem()
                {
                    Id = itemData.ItemId,
                };
                _playerInventory.AddItem<BlockItem>(blockItem, itemData.Amount);
            }
        }
    }
    
    private void OnDestroy()
    {
        var allItems = _playerInventory.GetAllItems<IItem>();
        _saveData.Items.Clear();
        foreach (var item in allItems)
        {
            _saveData.Items.Add(new InventoryItemData()
            {
                ItemId = item.Id,
                Amount = _playerInventory.GetItemQuantity(item.Id)
            });
        }
        _saveLoadSystem.SaveData(_saveData);
    }
}