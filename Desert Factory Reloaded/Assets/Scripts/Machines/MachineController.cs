using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour, ITickable
{
    public float delay;
    int ticks;

    public Storage inventory;
    public Recipe[] recipes;

    void Start()
    {
        inventory = new Storage(8);
        // TODO: InventorySlotUIFiller.FillInventory();
        GameTimer.Join(this);
    }
    public void ReceiveTick()
    {
        ticks++;
        if (ticks * 0.2f >= delay)
        {
            ticks = 0;
            Craft();
        }
    }

    void Craft()
    {
        foreach (Recipe recipe in recipes)
        {
            if (inventory.Contains(recipe.itemsIn))
            {
                inventory.Remove(recipe.itemsIn);
                inventory.Add(recipe.itemsOut);
            }
        }
    }
}