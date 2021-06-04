using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            WorldSaver.SaveWorld();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            WorldSaver.ClearSave();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            WorldSaver.LoadWorld(Application.dataPath + "/Saves/WORLDSAVE02.06.2021.bingus");
        }
    }
}