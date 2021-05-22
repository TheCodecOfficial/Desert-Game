using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTest : MonoBehaviour
{
    public Item item;

    Storage storage;
    void Start()
    {
        storage = new Storage(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            storage.Add(item);
            storage.Print();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            storage.Add(new ItemStack(item, 3));
            storage.Print();
        }
    }
}