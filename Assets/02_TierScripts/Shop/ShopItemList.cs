using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShopItem
{
    public string ItemId;
    public int Buy_Gold;

    public bool IsEmpty => string.IsNullOrWhiteSpace(ItemId) || Buy_Gold <= 0;
}
public class ShopItemList : MonoBehaviour
{
    [SerializeField]private List<ShopItem> ItemList = new List<ShopItem>();
    

    public IReadOnlyList<ShopItem> Items => ItemList;

    public int ShopCount()
    {
        return ItemList.Count;
    }
    
}
