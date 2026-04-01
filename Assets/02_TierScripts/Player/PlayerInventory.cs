
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]List<string> Inventory = new List<string>();
    [SerializeField]Dictionary<string,int> CountByInventory = new Dictionary<string,int>();
    [SerializeField] private int slotCapacity = 40;
    [SerializeField] CatalogManager catalogManager;
    public event Action<int> Gold;
    public event Action Item;
    public event Action<int> Use_Potion;
    //public IReadOnlyDictionary<string, int> Inven_Count => CountByInventory;
    private readonly List<InventorySlotData> inventorySlots = new List<InventorySlotData>();
    public IReadOnlyList<InventorySlotData> InventorySlots => inventorySlots;
   
    int Player_Gold;
    int Max_Gold = 1000000;
    public int Get_Gold => Player_Gold;
    public int SlotCapacity => slotCapacity;
    private void Awake()
    {
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        inventorySlots.Clear();
        int safeCapacity = Mathf.Max(0, slotCapacity);
        for (int i = 0; i < safeCapacity; i++)
        {
            inventorySlots.Add(new InventorySlotData { ItemId = string.Empty, Amount = 0 });
        }
    }
    public void AddItem(ItemData item, int amount)
    {
        string itemid = item.ItemId.Trim();
        ItemCatalogEntry entry;
        
        if(catalogManager == null || !catalogManager.TryGetEntry(itemid, out entry))
        {
            Debug.LogWarning("catalogManager == null || !catalogManager.TryGetEntry(itemid, out entry)");
            return;
        }
        if(item.Type == ItemType.Gold)
        {
            Player_Gold = Mathf.Clamp(Player_Gold+(entry.value * amount) , 0 , Max_Gold);
            Debug.Log($"{item.ItemDisplayName}을 획득하여 {entry.value * amount}만큼의 골드를 얻었습니다.");
            Gold?.Invoke(Player_Gold);
        }
        else
        {
            if(!TryAddItem(itemid))
            {
                Debug.Log("아이템 보관 공간 부족");
                return;
            }
            int ResultAmount = 0;
            AddItemSlotData(itemid, amount, out ResultAmount);
            Debug.Log($"{entry.DisplayName}을 {amount-ResultAmount}개 획득하여 인벤토리에 보관하였습니다");
            for(int i = 0; i < amount - ResultAmount; i++)
            {
                Inventory.Add(item.ItemId);
            }
            
            IncreaseItemCount(itemid, amount - ResultAmount);
            
            Item?.Invoke();
        }

    }
    private void AddItemSlotData(string itemid, int amount, out int CurrentAmount)
    {

        if(!catalogManager.IsRegistered(itemid))
        {
            Debug.LogWarning("!catalogManager.IsRegistered(itemid)");
            CurrentAmount = 0;
            return;
        }
        int MaxStack = catalogManager.GetMaxStack(itemid);
        int TotalAmount = amount;
        CurrentAmount = TotalAmount;
        for(int i =0;i < inventorySlots.Count;i++)
        {
            if (inventorySlots[i].ItemId == string.Empty)
            {
                if(CurrentAmount <= MaxStack)
                {
                    inventorySlots[i] = new InventorySlotData { ItemId = itemid, Amount = CurrentAmount };
                    CurrentAmount = 0;
                }
                else
                {
                    inventorySlots[i] = new InventorySlotData { ItemId = itemid, Amount = MaxStack };
                    CurrentAmount -= MaxStack;
                }
            }
            else if (inventorySlots[i].ItemId == itemid)
            {
                if (inventorySlots[i].Amount == MaxStack) continue;
                int m_Amount = inventorySlots[i].Amount;
                if (CurrentAmount +m_Amount <= MaxStack)
                {
                    inventorySlots[i] = new InventorySlotData { ItemId = itemid, Amount = CurrentAmount + m_Amount };
                    CurrentAmount = 0;
                }
                else
                {
                    inventorySlots[i] = new InventorySlotData { ItemId = itemid, Amount = MaxStack };
                    CurrentAmount -= (MaxStack-m_Amount);
                }
            }

            if (CurrentAmount <= 0)
            {
                return;
            }
        }
        Debug.Log("인벤토리 공간이 부족하여 주울 수 있는 만큼 주웠습니다.");
    }

    public bool TryAddItem(string itemid)
    {
        
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].ItemId == itemid && 
                inventorySlots[i].Amount < catalogManager.GetMaxStack(itemid))
            {
                return true;
            }
            if (inventorySlots[i].ItemId == string.Empty) { return true; }
        }
        return false;
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

    public void Use_Item(int index, string ItemId)
    {
        if (inventorySlots[index].ItemId != ItemId)
        {
            Debug.Log("inventorySlots[index].ItemId != ItemId");
            return;
        }
        ItemCatalogEntry entry;
        InventorySlotData slot = inventorySlots[index];
        if(!catalogManager.TryGetEntry(ItemId, out entry))
        {
            Debug.Log("!catalogManager.TryGetEntry(ItemId, out entry)");
            return;
        }
        switch(entry.Category)
        {
            case ItemType.Potion:
                Use_Potion?.Invoke(entry.value);
                DecreaseItemCount(ItemId, 1);
                inventorySlots[index] =  slot.Amount == 1 ? 
                    new InventorySlotData { ItemId = string.Empty, Amount = 0 } :
                    new InventorySlotData { ItemId = inventorySlots[index].ItemId, Amount = inventorySlots[index].Amount - 1 };
                Inventory.Remove(slot.ItemId);
                Item?.Invoke();
                Debug.Log($"{entry.Id} 사용, {entry.value}만큼 체력 회복");
                break;
            default:
                Debug.Log("현재 사용 구현 X");
                break;

        }
        
    }
}
