/*using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotList : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text AmountText;

    public void Bind(PlayerInventory inventory, ItemData data)
    {
        if (string.IsNullOrEmpty(data.ItemDisplayName))
        {
            SetEmptyVisual();
            return;
        }
        if (ItemImage != null)
        {
            //hasCatalogEntry -> hasCatalogEntry ? entry.icon : null;
            Sprite IconSprite = data.UI_Image;
            ItemImage.enabled = IconSprite != null; // if문과 같음
            ItemImage.sprite = IconSprite;
        }

        if (AmountText != null)
        {
            int amount = inventory.GetItemCount(data.ItemDisplayName);
            AmountText.gameObject.SetActive(true);
            AmountText.text = amount > 1 ?
                amount.ToString() : String.Empty;
        }

    }

    private void SetEmptyVisual()
    {
        if (ItemImage != null)
        {
            ItemImage.enabled = false;
            ItemImage.sprite = null;
        }

        if (AmountText != null)
        {
            AmountText.text = string.Empty;
            AmountText.gameObject.SetActive(false);
        }

    }
}
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotList : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text AmountText;
    [SerializeField] ItemInfo Info_UI;
    
    private void Awake()
    {
        Info_UI = FindFirstObjectByType<ItemInfo>(FindObjectsInactive.Include);
        if(Info_UI == null)
        {
            Debug.Log("Info_UI == null");
        }
    }
    public void UI_Refersh(Sprite sprite, int amount)
    {
        ItemImage.sprite = sprite;
        ItemImage.enabled = true;
        AmountText.text = amount <= 1 ? string.Empty : amount.ToString();
    }
    public void Button_Click()
    {
        if (ItemImage == null)
        {
            Debug.LogWarning("ItemImage == null");
            return;
        }
        string text = string.IsNullOrWhiteSpace(AmountText.text) ? "1" : AmountText.text;
        Info_UI.Info(ItemImage.sprite, text);
        Info_UI.gameObject.SetActive(true);
        //Debug.Log("버튼동작완료");
    }
}
