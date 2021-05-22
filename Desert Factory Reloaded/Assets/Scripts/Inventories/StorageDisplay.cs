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

    public Machine machine;

    void Start()
    {
        inventory = machine.inventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.Add(items[0]);
            Display();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.Add(items[1]);
            Display();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.Add(items[2]);
            Display();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.Add(items[3]);
            Display();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Display();
        }
        Display();
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