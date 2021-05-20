using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class FPSInfo : MonoBehaviour
{
    const int n = 1024;
    float[] fpsvalues = new float[n];
    int index = 0;
    void Start()
    {
        for (int i = 0; i < n; i++)
        {
            fpsvalues[i] = 1f / Time.deltaTime;
        }
    }
    void Update()
    {
        fpsvalues[index] = 1f / Time.deltaTime;
        index++;
        index %= n;
        GetComponent<TextMeshProUGUI>().text = "FPS: " + GetAverageFPS() + "\nMin FPS: " + GetMinFPS();
    }

    float GetAverageFPS()
    {
        float sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += fpsvalues[i];
        }
        return sum / n;
    }

    float GetMinFPS()
    {
        return fpsvalues.Min();
    }
}