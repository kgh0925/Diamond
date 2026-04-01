using System.Collections.Generic;
using UnityEngine;

public class CatalogManager : MonoBehaviour
{
    [Header("Item Catalog [ 주의점 : DisplayName 한글 금지 ]")]
    [SerializeField]
    private ItemCatalogEntry[] itemCatalogEntries;

    private readonly Dictionary<string, ItemCatalogEntry> catalogById
        = new Dictionary<string, ItemCatalogEntry>();


    private void Awake()
    {
        EnsureCatalogNotEmptyForRuntime();
        BuildCatalogDictionary();
    }
    public bool TryGetEntry(string inputItemId, out ItemCatalogEntry outEntry)
    {
        outEntry = default;
        if (string.IsNullOrWhiteSpace(inputItemId))
        {
            return false;
        }

        return catalogById.TryGetValue(inputItemId.Trim(), out outEntry);
    }
 
    public bool IsRegistered(string itemId)
    {
        if (string.IsNullOrWhiteSpace(itemId))
        {
            return false;
        }

        return catalogById.ContainsKey(itemId.Trim());

    }

    public int GetMaxStack(string itemId)
    {

        if (!TryGetEntry(itemId, out ItemCatalogEntry entry))
        {
            return 0;
        }

        return entry.MaxStack <= 0 ? int.MaxValue : entry.MaxStack;
    }

    public string ResloveDisplayName(string itemId)
    {

        if (TryGetEntry(itemId, out ItemCatalogEntry entry)
            && !string.IsNullOrEmpty(entry.DisplayName))
        {
            return entry.DisplayName;
        }

        return string.IsNullOrWhiteSpace(itemId) ? string.Empty : itemId.Trim();
    }
    private void EnsureCatalogNotEmptyForRuntime()
    {
        if (itemCatalogEntries != null && itemCatalogEntries.Length > 0)
        {
            return;
        }

        itemCatalogEntries = new[]
        {
            new ItemCatalogEntry { Id= "potion", DisplayName = "Red Potion", MaxStack = 20,
            Category = ItemType.Potion, Icon = null}
        };
        Debug.LogWarning("[ItemCatalog]가 비어 있습니다, 목록을 작성해주세요 ~");
    }

    private void BuildCatalogDictionary()
    {
        catalogById.Clear();

        if (itemCatalogEntries == null)
        {
            Debug.Log("item 카탈로그가 비었음");
            return;
        }

        for (int i = 0; i < itemCatalogEntries.Length; i++)
        {
            ItemCatalogEntry entry = itemCatalogEntries[i];

            if (string.IsNullOrWhiteSpace(entry.Id))
            {
                Debug.LogWarning($"[ItemCatalogManager] 빈 id 항목, index = {i}");
                continue;
            }

            string normalizedId = entry.Id.Trim();
            if (catalogById.ContainsKey(normalizedId))
            {
                Debug.LogWarning($"[ItemCatalogManager] 중복 카탈로그 id,{normalizedId}");
                continue;
            }

            ItemCatalogEntry stored = entry;
            stored.Id = normalizedId;
            catalogById.Add(normalizedId, stored);
        }
    }
}
