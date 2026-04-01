
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]List<ItemData> Inventory = new List<ItemData>();
    [SerializeField]Dictionary<string,int> CountByInventory = new Dictionary<string,int>();
    [SerializeField] private int slotCapacity = 40;
    public event Action<int> Gold;
    public event Action<int, Sprite, int> Item;
    public IReadOnlyDictionary<string, int> Inven_Count => CountByInventory;
    int Player_Gold;
    int Max_Gold = 1000000;
    public int Get_Gold => Player_Gold;
    public int SlotCapacity => slotCapacity;
    public void AddItem(ItemData item, int amount)
    {
        if(item.ItemId == ItemType.Gold)
        {
            Player_Gold = Mathf.Clamp(Player_Gold+(item.value * amount) , 0 , Max_Gold);
            Debug.Log($"{item.ItemDisplayName}을 획득하여 {item.value * amount}만큼의 골드를 얻었습니다.");
            Gold?.Invoke(Player_Gold);
        }
        else
        {
            Debug.Log($"{item.ItemDisplayName}을 {amount}개 획득하여 인벤토리에 보관하였습니다");
            for(int i = 0; i < amount; i++)
            {
                Inventory.Add(item);
            }
            
            IncreaseItemCount(item.ItemDisplayName, amount);
            Item?.Invoke(Inventory_Index()-1, item.UI_Image, GetItemCount(item.ItemDisplayName));
        }

    }

    public int GetItemCount(string Name)
    {
        if(CountByInventory.TryGetValue(Name, out int count)) return count;
        return 0;
    }

    private void IncreaseItemCount(string itemId, int amount)
    {
        if (CountByInventory.TryGetValue(itemId, out int currentCount))
        {
            CountByInventory[itemId] = currentCount + amount;
        }
        else
        {
            CountByInventory[itemId] = amount;
        }
    }

    private void DecreaseItemCount(string itemId, int amount)
    {
        if (!CountByInventory.TryGetValue(itemId, out int currentCount)) return;

        int nextCount = currentCount - amount;
        if (nextCount <= 0)
        {   
            CountByInventory.Remove(itemId);
        }
        else
        {
            CountByInventory[itemId] = nextCount;
        }

    }
    public int Inventory_Index()
    {
        return CountByInventory.Count;
    }

    //UI에서 인벤토리를 열 경우 로그에 함께 출력을 위해 Public 선언
    public void PrintInventory()
    {

        Debug.Log("==== [Inventory : Dictionary ] ====");
        foreach (KeyValuePair<string, int> pair in CountByInventory)
        {
            Debug.Log($"{pair.Key} : {pair.Value}");
        }
    }
}
