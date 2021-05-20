using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    static List<Tickable> objectsToTick;

    float timer;
    void Start(){
        objectsToTick = new List<Tickable>();
    }

    public static void Join(Tickable obj){
        objectsToTick.Add(obj);
    }

    void Update(){
        timer += Time.deltaTime;
        KindaExpensiveFunction();
        if (timer >= 1f){
            Tick();
            timer = 0;
        }
    }

    static void Tick(){
        foreach (Tickable t in objectsToTick){
            t.ReceiveTick();
        }
    }

    public static void KindaExpensiveFunction(){
        for (int i = 0; i < 1000; i++){
            float x = Mathf.Sqrt(Mathf.Sqrt(i));
        }
    }
}
public interface Tickable {
    void ReceiveTick();
}