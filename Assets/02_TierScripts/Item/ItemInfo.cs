using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text Amount;
    [SerializeField] TMP_Text ItemNameText;
    private int ItemIndex;

    [SerializeField] PlayerInventory playerInventory;
    public void Info(Sprite Image, string text, string ItemId, int index)
    {
        ItemIndex = index;
        Icon.sprite= Image;
        Amount.text = "Amount : " + text;
        ItemNameText.text = ItemId;
    }

    //Button Use
    public void Use()
    {
        playerInventory.Use_Item(ItemIndex, ItemNameText.text);
    }
}
