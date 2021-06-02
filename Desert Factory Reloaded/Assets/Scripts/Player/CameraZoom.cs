using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float min, max;
    public float zoomSpeed;
    float zoom;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        zoom += -Input.mouseScrollDelta.y;
        zoom = Mathf.Clamp(zoom, min, max);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime*zoomSpeed);
    }
}