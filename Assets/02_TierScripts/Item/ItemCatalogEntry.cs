using System;
using UnityEngine;

[Serializable]
public struct ItemCatalogEntry
{

    public string Id;
    public string DisplayName;
    public int MaxStack;
    public ItemType Category;
    public Sprite Icon;
    public int value;
}
