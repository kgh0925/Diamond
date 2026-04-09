using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopItemList ShopList;
    [SerializeField] InputAction Input_Shop;
    [SerializeField] Image Shop_UI;
    [Header("Slot UI Perent")]
    [SerializeField] RectTransform SlotContainer;
    [SerializeField] private CatalogManager ItemCatalogManager;
    [SerializeField] private ShopUISlot SlotPrefeb;
    [SerializeField] private List<ShopUISlot> slotLists;
    [SerializeField] ShopIN ShopInState;
    [SerializeField] ShopBuy Buy_UI;

    bool IsOpened;

    private void Awake()
    {
        Shop_UI.gameObject.SetActive(false);

    }
    private void Start()
    {
        BuildSlotViews();
        RedrawAllSlots();
    }
    private void OnEnable()
    {
        Input_Shop.Enable();
    }
    private void OnDisable()
    {
        Input_Shop.Disable();
    }

    private void Update()
    {
        if (Input_Shop.WasPerformedThisFrame() && ShopInState.Area)
        {
            if (IsOpened)
            {
                Shop_UI.gameObject.SetActive(false);
                Buy_UI.gameObject.SetActive(false);
            }
            else
            {
                Shop_UI.gameObject.SetActive(true);
            }
            IsOpened = !IsOpened;
        }
        if(!ShopInState.Area && IsOpened)
        {
            IsOpened = !IsOpened;
            Shop_UI.gameObject.SetActive(false);
            Buy_UI.gameObject.SetActive(false);
        }
    }
    public void Close_UI()
    {
        Shop_UI.gameObject.SetActive(false);
        IsOpened = false;
    }


    private void BuildSlotViews()
    {
        if (SlotContainer == null || SlotPrefeb == null)
        {
            Debug.Log("SlotContainer == null || SlotPrefeb == null");
            return;
        }
        if (ShopList == null)
        {
            Debug.Log("Inventory == null");
            return;
        }
        for (int childIndex = SlotContainer.childCount - 1; childIndex >= 0; childIndex--)
        {
            Destroy(SlotContainer.GetChild(childIndex).gameObject);
        }
        slotLists.Clear();

        int capacity = Mathf.Max(0, ShopList.ShopCount());
        for (int slotindex = 0; slotindex < capacity; slotindex++)
        {
            ShopUISlot slotInstance = Instantiate(SlotPrefeb, SlotContainer);
            slotInstance.gameObject.name = $"Slot_{slotindex:D2}";
            slotLists.Add(slotInstance);
        }
    }

    private void RedrawAllSlots()
    {
        if (Input_Shop == null || slotLists.Count == 0 || ItemCatalogManager == null) return;
        IReadOnlyList<ShopItem> slots = ShopList.Items;
        for (int viewIndex = 0; viewIndex < slotLists.Count; viewIndex++)
        {
            ShopItem slotData = viewIndex < slots.Count ? slots[viewIndex]
                : new ShopItem { ItemId = string.Empty, Buy_Gold = 0 };
            slotLists[viewIndex].Bind(slotData, ItemCatalogManager);
        }
    }
}