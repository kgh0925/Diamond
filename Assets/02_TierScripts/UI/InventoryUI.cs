/*using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] PlayerInventory inventory;
    [SerializeField] InputAction Inventory;
    [SerializeField] Image Inventory_UI;
    [SerializeField] TMP_Text Gold_Text;
    [Header("Slot UI Perent")]
    [SerializeField] RectTransform SlotContainer;
    [SerializeField] private SlotList SlotPrefeb;
    [SerializeField] private List<SlotList> slotLists;
    

    public event Action<bool> Inventory_Open;
    bool IsOpened;
    
    private void Awake()
    {
        Inventory_UI.gameObject.SetActive(false);
        
    }
    private void Start()
    {
        BuildSlotViews();
        //RedrawAllSlots();
    }
    private void OnEnable()
    {
        Inventory.Enable();
        inventory.Gold += ReFersh_Inventory_Gold;
        inventory.Item += ReFersh_Inventory_Index_UI;
    }
    private void OnDisable()
    {
        Inventory.Disable();
        inventory.Gold -= ReFersh_Inventory_Gold;
        inventory.Item -= ReFersh_Inventory_Index_UI;
    }

    private void Update()
    {
        if(Inventory.WasPerformedThisFrame())
        {
            if(IsOpened)
            {
                Close_UI();
            }
            else
            {
                Open_UI();
            }
            
        }
    }
    public void Close_UI()
    {
        Inventory_UI.gameObject.SetActive(false);
        IsOpened = !IsOpened;
        Inventory_Open?.Invoke(true);
    }
    public void Open_UI()
    {
        Inventory_UI.gameObject.SetActive(true);
        Inventory_Open?.Invoke(false);
        inventory.PrintInventory();
        IsOpened = !IsOpened;
    }


    public void ReFersh_Inventory_Gold(int gold)
    {
        Gold_Text.text = gold.ToString();
    }
    public void ReFersh_Inventory_Index_UI(int index, Sprite sprite)
    {
        if (inventory == null || slotLists.Count <= 0) return;
        //IReadOnlyList<ItemData> slots = 
    }
    private void RedrawAllSlots()
    {
        if (Inventory == null || slotLists.Count == 0) return;
        for (int viewIndex = 0; viewIndex < slotLists.Count; viewIndex++)
        {
            ItemData slotData = viewIndex < inventory.Inven.Count ? slots[viewIndex]
                : new ItemData { ItemDisplayName = string.Empty };
            slotLists[viewIndex].Bind(inventory, slotData);
        }
    }
    private void BuildSlotViews()
    {
        if (SlotContainer == null || SlotPrefeb == null)
        {
            Debug.Log("SlotContainer == null || SlotPrefeb == null");
            return;
        }
        if (Inventory == null)
        {
            Debug.Log("Inventory == null");
            return;
        }
        for (int childIndex = SlotContainer.childCount - 1; childIndex >= 0; childIndex--)
        {
            Destroy(SlotContainer.GetChild(childIndex).gameObject);
        }
        slotLists.Clear();

        int capacity = Mathf.Max(0, inventory.SlotCapacity);
        for (int slotindex = 0; slotindex < capacity; slotindex++)
        {
            SlotList slotInstance = Instantiate(SlotPrefeb, SlotContainer);
            slotInstance.gameObject.name = $"Slot_{slotindex:D2}";
            slotLists.Add(slotInstance);
        }
    }
}
*/

using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] PlayerInventory inventory;
    [SerializeField] InputAction Input_Inventory;
    [SerializeField] Image Inventory_UI;
    [SerializeField] TMP_Text Gold_Text;
    [Header("Slot UI Perent")]
    [SerializeField] RectTransform SlotContainer;
    [SerializeField] private CatalogManager ItemCatalogManager;
    [SerializeField] private InventoryUISlot SlotPrefeb;
    [SerializeField] private List<InventoryUISlot> slotLists;


    public event Action<bool> Inventory_Open;
    bool IsOpened;

    private void Awake()
    {
        Inventory_UI.gameObject.SetActive(false);
    }
    private void Start()
    {
        BuildSlotViews();
        RedrawAllSlots();
    }
    private void OnEnable()
    {
        Input_Inventory.Enable();
        inventory.Gold += ReFersh_Inventory_Gold;
        inventory.Item += ReFersh_Inventory_UI;
    }
    private void OnDisable()
    {
        Input_Inventory.Disable();
        inventory.Gold -= ReFersh_Inventory_Gold;
        inventory.Item -= ReFersh_Inventory_UI;
    }

    private void Update()
    {
        if (Input_Inventory.WasPerformedThisFrame())
        {
            if (IsOpened)
            {
                Inventory_UI.gameObject.SetActive(false);
            }
            else
            {
                Inventory_UI.gameObject.SetActive(true);
                inventory.PrintInventory();
            }
            IsOpened = !IsOpened;
        }
    }
    public void Close_UI()
    {
        Inventory_UI.gameObject.SetActive(false);
        IsOpened = false;
    }
    
    public void ReFersh_Inventory_UI()
    {
        RedrawAllSlots();
    }
    
    public void ReFersh_Inventory_Gold(int gold)
    {
        Gold_Text.text = gold.ToString();
    }
    /*
    public void ReFersh_Inventory_Index_UI(int index, Sprite sprite, int amount)
    {
        slotLists[index].UI_Refersh(sprite, amount);
        Debug.Log($"Index : {index} ");
    }*/
    private void BuildSlotViews()
    {
        if (SlotContainer == null || SlotPrefeb == null)
        {
            Debug.Log("SlotContainer == null || SlotPrefeb == null");
            return;
        }
        if (inventory == null)
        {
            Debug.Log("Inventory == null");
            return;
        }
        for (int childIndex = SlotContainer.childCount - 1; childIndex >= 0; childIndex--)
        {
            Destroy(SlotContainer.GetChild(childIndex).gameObject);
        }
        slotLists.Clear();

        int capacity = Mathf.Max(0, inventory.SlotCapacity);
        for (int slotindex = 0; slotindex < capacity; slotindex++)
        {
            InventoryUISlot slotInstance = Instantiate(SlotPrefeb, SlotContainer);
            slotInstance.gameObject.name = $"Slot_{slotindex:D2}";
            slotLists.Add(slotInstance);
        }
    }

    private void RedrawAllSlots()
    {
        if (Input_Inventory == null || slotLists.Count == 0 || ItemCatalogManager == null) return;
        IReadOnlyList<InventorySlotData> slots = inventory.InventorySlots;
        for (int viewIndex = 0; viewIndex < slotLists.Count; viewIndex++)
        {
            InventorySlotData slotData = viewIndex < slots.Count ? slots[viewIndex]
                : new InventorySlotData { ItemId = string.Empty , Amount = 0 };
            slotLists[viewIndex].Bind(slotData, ItemCatalogManager, viewIndex);
        }
    }
}