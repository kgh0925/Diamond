using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TMP_Text Amount;
    [SerializeField] TMP_Text ItemNameText;

    public void Info(Sprite Image, string text)
    {
        Icon.sprite= Image;
        Amount.text = "Amount : " + text;
    }
}
