using System;
using System.Collections.Generic;

[Serializable]
public class InventorySaveData
{
    public List<InventoryItemData> Items;
}

[Serializable]
public class InventoryItemData
{
    public long ItemId;
    public int Amount;
}

