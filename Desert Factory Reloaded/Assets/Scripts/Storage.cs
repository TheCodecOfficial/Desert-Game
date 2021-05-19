using System.Collections.Generic;
public class Storage
{
    public int size;
    public int maxAmount;

    public List<ItemStack> inventory;

    public Storage(int size)
    {
        this.size = size;
        maxAmount = 64; // Minecraft moment
        inventory = new List<ItemStack>();
    }

    public bool HasFreeSpace()
    {
        return (inventory.Count < size);
    }

    public bool HasSpace(Item item)
    {
        if (HasFreeSpace()) return true;
        foreach (ItemStack stack in inventory)
        {
            if (stack.item == item && stack.amount < maxAmount) return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return !HasFreeSpace();
    }
}