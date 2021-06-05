using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Item[] items;
    public Machine[] machines;

    static DataBase itemDB, machineDB;
    private void Start() {
        itemDB = new DataBase(items);
        machineDB = new DataBase(machines);
    }

    public static Item GetItem(string name){
        if (itemDB.data.ContainsKey(name)){
            return (Item)itemDB.data[name];
        }
         
        Debug.LogError("Item " + name + " not found");
        return null;
    }

    public static Machine GetMachine(string name){
        if (machineDB.data.ContainsKey(name)){
            return (Machine)machineDB.data[name];
        }
         
        Debug.LogError("Machine " + name + " not found");
        return null;
    }

    public static Machine GetRandomMachine(){
        List<ScriptableObject> values = new List<ScriptableObject>(machineDB.data.Values);
        return (Machine) values[Random.Range(0, values.Count)];
    }
}