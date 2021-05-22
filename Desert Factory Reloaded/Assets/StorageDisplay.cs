using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageDisplay : MonoBehaviour
{

    Storage inventory;

    public Item[] items;

    public Transform[] slots;

    void Start()
    {
        inventory = new Storage(slots.Length);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.Add(items[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.Add(items[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.Add(items[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.Add(items[3]);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            inventory.Print();
        }
        Display();
    }

    void Display()
    {
        int index = 0;
        foreach (Item item in inventory.inventory.Keys)
        {
            int slotsNeeded = Mathf.FloorToInt(inventory.inventory[item] / 4f);
            for (int i = 0; i < slotsNeeded; i++){
                DisplaySlot(slots[index], item, 4);
                index++;
            }
            DisplaySlot(slots[index], item, inventory.inventory[item] - 4 * slotsNeeded);

            //index++;
        }
    }

    void DisplaySlot(Transform slot, Item item, int amount)
    {
        slot.GetChild(0).GetComponent<Image>().sprite = item.sprite;
        slot.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + amount;
    }
}