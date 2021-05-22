using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    static List<ITickable> objectsToTick;

    float timer;

    public static void Join(ITickable obj){
        if (objectsToTick == null) objectsToTick = new List<ITickable>();
        objectsToTick.Add(obj);
    }

    void Update(){
        timer += Time.deltaTime;
        if (timer >= 0.2f){
            Tick();
            timer = 0;
        }
    }

    static void Tick(){
        foreach (ITickable t in objectsToTick){
            t.ReceiveTick();
        }
    }
}
public interface ITickable {
    void ReceiveTick();
}