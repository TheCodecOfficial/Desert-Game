using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUIFiller : MonoBehaviour
{
    public GameObject SlotUIPrefab;
    static GameObject SlotUIObj;
    private void Awake() {
        SlotUIObj = SlotUIPrefab;
    }
    public static void FillInventory(Transform parent, Storage inventory){

        for (int i = 0; i < inventory.maxSlots; i++){
            Instantiate(SlotUIObj, parent);
        }
    }
}