using System;
using System.Collections.Generic;

[Serializable]
public class ItemEntry
{
    public IItem Item;
    public int   Amount;
}
    
public class Inventory : IInventory
{
    /// <summary>All the items in the inventory.</summary>
    private Dictionary<long, ItemEntry> _items = new Dictionary<long, ItemEntry>();

    public IItem GetItem<T>(long id) where T : IItem
    {
        if (_items.TryGetValue(id, out var itemEntry))
        {
            itemEntry.Amount--;
            if (itemEntry.Amount <= 0)
            {
                _items.Remove(id);
            }
            return itemEntry.Item;
        }
            
        return null;
    }

    public IItem[] GetAllItems<T>() where T : IItem
    {
        var items = new List<IItem>();
        foreach (var itemEntry in _items.Values)
        {
            items.Add(itemEntry.Item);
        }

        return items.ToArray();
    }

    public int GetItemQuantity(long id)
    {
        if (_items.TryGetValue(id, out var itemEntry))
        {
            return itemEntry.Amount;
        }

        return 0;
    }

    public void AddItem<T>(IItem item, int quantity = 1) where T : IItem
    {
        if (_items.TryGetValue(item.Id, out var itemEntry))
        {
            itemEntry.Amount += quantity;
        }
        else
        {
            itemEntry = new ItemEntry
            {
                Item   = item,
                Amount = quantity
            };
            _items.Add(item.Id, itemEntry);
        }
    }
}