using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageSlotUI : MonoBehaviour
{
    Image icon;
    TextMeshProUGUI amountText;
    TextMeshProUGUI nameText;
    void Start()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        Clear();
    }

    public void Clear()
    {
        icon.enabled = false;
        amountText.text = "";
        nameText.text = "";
    }

    public void Display(ItemStack items)
    {
        icon.enabled = true;
        icon.sprite = items.item.sprite;
        amountText.text = "" + items.amount;
        nameText.text = items.item.name;
    }
}