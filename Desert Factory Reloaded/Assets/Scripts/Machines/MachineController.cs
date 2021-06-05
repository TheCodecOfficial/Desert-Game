using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour, ITickable
{
    public float delay;
    int ticks;
    public Storage inventory;
    public Storage outputInventory;
    public int outputDirection;
    int dx, dy;
    public Recipe[] recipes;
    int x, y;
    public void Setup(int _x, int _y, int direction)
    {
        x = _x;
        y = _y;
        outputDirection = direction;

        // Get output inventory
        CalculateOffset();
        Tile outputTile = WorldData.GetTile(x + dx, y + dy);
        if (outputTile.type == 2){
            MachineTile outputMachineTile = (MachineTile)outputTile;
            outputInventory = outputMachineTile.machineController.inventory;
        }
    }
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
    void CalculateOffset()
    {
        // 0: +X
        if (outputDirection == 0) dx = 1;
        // 0: +Y
        if (outputDirection == 1) dy = 1;
        // 0: -X
        if (outputDirection == 2) dx = -1;
        // 0: +Y
        if (outputDirection == 3) dy = -1;
    }
}