using UnityEngine;
using System.Collections.Generic;

public class Storage
{
    int slots { get { return GetSlots(); } }
    public int maxSlots, slotSize;
    Dictionary<Item, int> data;

    public Storage(int _maxSlots)
    {
        maxSlots = _maxSlots;
        slotSize = 64; // Minecraft moment

        data = new Dictionary<Item, int>();
    }
    #region Contains
    public bool Contains(ItemStack items)
    {
        return (data.ContainsKey(items.item) && data[items.item] >= items.amount);
    }
    public bool Contains(Item item)
    {
        return Contains(new ItemStack(item, 1));
    }
    public bool Contains(ItemStack[] items)
    {
        foreach (ItemStack stack in items)
        {
            if (!Contains(stack)) return false;
        }
        return true;
    }
    #endregion

    #region Space Checking
    public bool HasFreeSlot()
    {
        return (slots < maxSlots);
    }

    public bool HasSpace(ItemStack items)
    {
        if (HasFreeSlot()) return true;

        if (data.ContainsKey(items.item))
        {
            int slotsbefore = GetSlots();
            data[items.item] += items.amount;
            int slotsafter = GetSlots();
            data[items.item] -= items.amount;
            return (slotsbefore == slotsafter);
        }
        else return false;
    }

    public bool HasSpace(Item item)
    {
        return HasSpace(new ItemStack(item, 1));
    }
    #endregion

    #region Add
    public void Add(ItemStack items)
    {
        if (data.ContainsKey(items.item))
        {
            if (HasSpace(items))
            {
                data[items.item] += items.amount;
            }
        }
        else if (HasFreeSlot())
        {
            data.Add(items.item, items.amount);
        }
    }

    public void Add(Item item)
    {
        Add(new ItemStack(item, 1));
    }

    public void Add(ItemStack[] items){
        foreach (ItemStack stack in items){
            Add(stack);
        }
    }

    #endregion

    #region Remove
    public void Remove(ItemStack items){
        if (Contains(items)){
            data[items.item] -= items.amount;
        }
    }

    public void Remove(Item item){
        Remove(new ItemStack(item, 1));
    }

    public void Remove(ItemStack[] items){
        foreach (ItemStack stack in items){
            Remove(stack);
        }
    }

    #endregion
    int GetSlots()
    {
        int sum = 0;
        foreach (Item item in data.Keys)
        {
            sum += Mathf.CeilToInt(data[item] / (float)slotSize);
        }
        return sum;
    }
    public List<ItemStack> GetDistribution()
    {
        List<ItemStack> distribution = new List<ItemStack>();
        foreach (Item item in data.Keys)
        {
            int slotsNeeded = Mathf.CeilToInt(data[item] / (float)slotSize);
            for (int i = 0; i < slotsNeeded; i++)
            {
                int amount = (i == slotsNeeded - 1) ? data[item] - slotSize * (slotsNeeded - 1) : slotSize;
                distribution.Add(new ItemStack(item, amount));
            }
        }
        return distribution;
    }
    public void Print()
    {
        string s = "Storage contains: ";
        foreach (Item item in data.Keys)
        {
            s += data[item] + " x " + item.name + ", ";
        }
        s += " in " + GetSlots() + " slots.";
        Debug.Log(s);
    }
}