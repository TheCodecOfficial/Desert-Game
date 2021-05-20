using UnityEngine;
using System.Collections.Generic;
public class Storage
{
    int slots
    {
        get
        {
            return GetSlots();
        }
    }
    int maxSlots, slotSize;
    int size;

    Dictionary<Item, int> inventory;

    public Storage(int _maxSlots)
    {
        maxSlots = _maxSlots;
        slotSize = 4; // Minecraft moment
        size = 0;

        inventory = new Dictionary<Item, int>();
    }

    public bool HasFreeSlot()
    {
        return (slots < maxSlots);
    }

    public bool HasSpace(ItemStack items)
    {
        if (HasFreeSlot()) return true;

        if (inventory.ContainsKey(items.item))
        {
            int slotsbefore = GetSlots();
            inventory[items.item] += items.amount;
            int slotsafter = GetSlots();
            inventory[items.item] -= items.amount;
            return (slotsbefore == slotsafter);
        }
        else return false;
    }

    public bool HasSpace(Item item)
    {
        return HasSpace(new ItemStack(item, 1));
    }

    int GetSlots()
    {
        int sum = 0;
        foreach (Item item in inventory.Keys)
        {
            sum += Mathf.CeilToInt(inventory[item] / (float)slotSize);
        }
        return sum;
    }

    public void Add(ItemStack items)
    {
        if (inventory.ContainsKey(items.item))
        {
            if (HasSpace(items))
            {
                inventory[items.item] += items.amount;
            }
        }
        else if (HasFreeSlot())
        {
            inventory.Add(items.item, items.amount);
        }
    }

    public void Add(Item item)
    {
        Add(new ItemStack(item, 1));
    }

    public void Print()
    {
        string s = "Storage contains: ";
        foreach (Item item in inventory.Keys)
        {
            s += inventory[item] + " x " + item.name + ", ";
        }
        s += " in " + GetSlots() + " slots.";
        Debug.Log(s);
    }
}