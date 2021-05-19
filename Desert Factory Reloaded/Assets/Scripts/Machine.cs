using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour, Tickable
{
    public float frequency;
    float time;


    void Start(){
        GameTimer.Join(this);
    }
    void Update()
    {
        
    }

    public void ReceiveTick(){
        time += 1f;
    }
}