using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text AmountText;
    [SerializeField] ItemInfo Info_UI;
    private ItemCatalogEntry Entry;
    private int SlotIndex;
    private void Awake()
    {
        Info_UI = FindFirstObjectByType<ItemInfo>(FindObjectsInactive.Include);
        if (Info_UI == null)
        {
            Debug.Log("Info_UI == null");
        }
    }
    public void Bind(InventorySlotData data, CatalogManager catalogManager, int Index)
    {
        if (data.IsEmpty)
        {
            SetEmptyVisual();
            return;
        }
        /*        if (ItemImage != null)
                {
                    Sprite IconSprite = data.UI_Image;
                    ItemImage.enabled = IconSprite != null;
                    ItemImage.sprite = IconSprite;
                }

                if (AmountText != null)
                {
                    int amount = inventory.GetItemCount(data.ItemDisplayName);
                    AmountText.gameObject.SetActive(true);
                    AmountText.text = amount > 1 ?
                        amount.ToString() : String.Empty;
                }*/
        SlotIndex = Index;
        ItemCatalogEntry entry = default;
        bool hascatalogentry = catalogManager != null && catalogManager.TryGetEntry(data.ItemId, out entry);
        if (ItemImage != null && hascatalogentry)
        {
            Sprite icon = entry.Icon;
            ItemImage.enabled = true;
            ItemImage.sprite = icon;
            if (AmountText == null) return;
            AmountText.gameObject.SetActive(true);
            AmountText.text = data.Amount <= 1 ? string.Empty : data.Amount.ToString();
        }
        Entry = entry;
        //Debug.Log("바인드동작");

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
    public void Button_Click()
    {
        if (ItemImage == null)
        {
            Debug.LogWarning("ItemImage == null");
            return;
        }
        string text = string.IsNullOrWhiteSpace(AmountText.text) ? "1" : AmountText.text;
        Info_UI.Info(ItemImage.sprite, text, Entry.Id, SlotIndex);
        Info_UI.gameObject.SetActive(true);
        //Debug.Log("버튼동작완료");
    }
}

