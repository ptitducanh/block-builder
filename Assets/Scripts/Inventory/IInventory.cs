public interface IInventory
{
    /// <summary>
    /// Get an item from the inventory by id.
    /// If there is no item with the id, return null.
    /// If player get the last item of the inventory, remove it from the inventory.
    /// </summary>
    public IItem GetItem<T>(long id) where T : IItem;

    /// <summary>
    /// Get all the item from the inventory.
    /// Note: This function is quite heavy, use it with care.
    /// </summary>
    public IItem[] GetAllItems<T>() where T : IItem;

    /// <summary>
    /// Get the quantity of an item in the inventory.
    /// </summary>
    public int GetItemQuantity(long id);
        
    /// <summary>
    /// Add an item to the inventory.
    /// If the item is already in the inventory, add the amount of the item.
    /// </summary>
    public void AddItem<T>(IItem item, int quantity = 1) where T : IItem;
}