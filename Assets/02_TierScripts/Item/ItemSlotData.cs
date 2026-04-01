using System;
using UnityEngine;

[Serializable]
public struct InventorySlotData
{
    public string ItemId;
    public int Amount;

    public bool IsEmpty => string.IsNullOrEmpty(ItemId) || Amount <= 0;
}
