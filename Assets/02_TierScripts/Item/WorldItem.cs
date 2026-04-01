using System;
using UnityEngine;
[Serializable]
public enum ItemType
{
    Potion,
    Weapon,
    Armor,
    Gold,
    Job
}
[Serializable]
public struct ItemData
{
    public ItemType ItemId;
    public string ItemDisplayName;
    public int value;
    public Sprite UI_Image;
}
public class WorldItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemData ItemData;
    //플레이어 인벤토리에도 UI를 위해 아이템 정보에 Sprite를 가지고 있는게 좋은가?
    //맞다면 Sprite를 ItemData에 옮기는게 더 좋은 방향성인지? 질문!
    [SerializeField] int amount;
    private void Awake()
    {
        if (amount <= 0) amount = 1;
    }
    public string GetDiscription()
    {
        return ItemData.ItemDisplayName + "발견 ! 주울려면 상호작용 키를 눌러주세요.";
    }

    public void Interact(PlayerInteract player)
    {
        PlayerInventory Inventory = player.GetComponent<PlayerInventory>();
        if(Inventory != null)
        {
            Inventory.AddItem(ItemData, amount);
            gameObject.SetActive(false);
        }

    }
}
