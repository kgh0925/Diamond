using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuy : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text BuyGold;
    [SerializeField] TMP_Text ItemNameText;
    private int Gold;
    private string ItemId;

    [SerializeField] PlayerInventory playerInventory;
    public void Buy_Info(Sprite Image, int gold, string ItemId)
    {
        Gold = gold;
        this.ItemId = ItemId;
        Icon.sprite = Image;
        BuyGold.text = "Gold : " + gold.ToString();
        ItemNameText.text = ItemId;
    }

    //Button Use
    public void Buy()
    {
        if(playerInventory.TryAddItem(ItemId))
        {
            if(playerInventory.BuyItem(ItemId, Gold))
            {
                Debug.Log("성공적으로 아이템을 구매하였습니다.");
                return;
            }
            else
            {
                Debug.Log("돈이 부족하여 구매할 수 없습니다.");
            }
        }
        else
        {
            Debug.Log("공간이 부족하여 구매할 수 없습니다.");
        }
            

    }
}
