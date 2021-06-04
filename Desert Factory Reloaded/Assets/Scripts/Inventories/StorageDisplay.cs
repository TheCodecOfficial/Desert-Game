using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageDisplay : MonoBehaviour
{
    Storage inventory;

    public Item[] items;

    public StorageSlotUI[] slots;

    private void Start() {
        inventory = new Storage(8);
        InventorySlotUIFiller.FillInventory(transform, inventory);
    }
    void Display()
    {
        foreach (StorageSlotUI slot in slots)
        {
            slot.Clear();
        }

        List<ItemStack> distribution = inventory.GetDistribution();
        int index = 0;
        foreach (ItemStack stack in distribution)
        {
            slots[index].Display(stack);
            index++;
        }
    }
}