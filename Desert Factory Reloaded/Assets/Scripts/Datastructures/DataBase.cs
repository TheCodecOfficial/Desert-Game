using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataBase
{
    public Dictionary<string, ScriptableObject> data;
    public DataBase(ScriptableObject[] _data){
        data = new Dictionary<string, ScriptableObject>();
        foreach (ScriptableObject obj in _data){
            data.Add(obj.name, obj);
        }
    }

    public void Print(){
        foreach (string name in data.Keys){
            Debug.Log(name);
        }
    }
}