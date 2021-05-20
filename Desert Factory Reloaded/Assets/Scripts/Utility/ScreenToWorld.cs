using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorld
{
	public static Vector2Int GetMousePoint(){
		Camera cam = Camera.main;
		if (cam.orthographic) return GetMousePointOrtho(cam);
		else return GetMousePointPersp(cam);
	}
    static Vector2Int GetMousePointPersp(Camera cam)
    {
        Vector3 camPos = cam.transform.position;
        Vector2 mousePos = Input.mousePosition;
        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1f));
        Vector3 dir = camPos - pos;

        float alpha = -camPos.y / dir.y;

        Vector3 adjustedDir = alpha * dir;
        Vector3 adjustedPos = adjustedDir + camPos;

        return new Vector2Int(Mathf.RoundToInt(adjustedPos.x), Mathf.RoundToInt(adjustedPos.z));
    }
    static Vector2Int GetMousePointOrtho(Camera cam)
    {
        Vector3 camPos = cam.transform.position;
        Vector2 mousePos = Input.mousePosition;
        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1f));
        Vector3 dir = cam.transform.rotation * Vector3.forward;

        float alpha = -pos.y / dir.y;

        Vector3 adjustedDir = alpha * dir;
        Vector3 adjustedPos = pos + adjustedDir;
        return new Vector2Int(Mathf.RoundToInt(adjustedPos.x), Mathf.RoundToInt(adjustedPos.z));
    }
}