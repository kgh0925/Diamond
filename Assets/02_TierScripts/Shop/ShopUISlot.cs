using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ShopUISlot : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text BuyGold;
    [SerializeField] ShopBuy Buy_UI;
    private ShopItem MyShopItem;

    private void Awake()
    {
        Buy_UI = FindFirstObjectByType<ShopBuy>(FindObjectsInactive.Include);
        if (Buy_UI == null)
        {
            Debug.Log("Buy_UI == null");
        }
    }
    public void Bind(ShopItem data, CatalogManager catalogManager)
    {
        if (data.IsEmpty)
        {
            SetEmptyVisual();
            return;
        }
        ItemCatalogEntry entry = default;
        bool hascatalogentry = catalogManager != null && catalogManager.TryGetEntry(data.ItemId, out entry);
        if (ItemImage != null && hascatalogentry)
        {
            Sprite icon = entry.Icon;
            ItemImage.enabled = true;
            ItemImage.sprite = icon;
            if (BuyGold == null) return;
            BuyGold.gameObject.SetActive(true);
            BuyGold.text = data.Buy_Gold.ToString();
            MyShopItem = data;
        }
        //Debug.Log("바인드동작");

    }

    private void SetEmptyVisual()
    {
        if (ItemImage != null)
        {
            ItemImage.enabled = false;
            ItemImage.sprite = null;
        }

        if (BuyGold != null)
        {
            BuyGold.text = string.Empty;
            BuyGold.gameObject.SetActive(false);
        }

    }

    public void Button_Click()
    {
        if (ItemImage == null)
        {
            Debug.LogWarning("ItemImage == null");
            return;
        }
        Buy_UI.Buy_Info(ItemImage.sprite, MyShopItem.Buy_Gold, MyShopItem.ItemId);
        Buy_UI.gameObject.SetActive(true);
        //Debug.Log("버튼동작완료");
    }
}

