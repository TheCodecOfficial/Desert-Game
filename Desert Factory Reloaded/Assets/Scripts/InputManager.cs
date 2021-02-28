using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

	public Transform selectionOverlay;

    void Start()
    {
        
    }

    void Update()
    {
		if (ScreenToWorld.MouseOverGround()) {
			Vector2 mousePos = ScreenToWorld.GetMousePoint();
			Vector3 pos = new Vector3(mousePos.x, 0.01f, mousePos.y);
			selectionOverlay.position = pos;
		}
    }

	private void OnDrawGizmos() {
		if (ScreenToWorld.MouseOverGround()) {
			Vector2 mousePoint = ScreenToWorld.GetMousePoint();

			Vector3 pos = new Vector3(mousePoint.x, 0.01f, mousePoint.y);

			Gizmos.DrawCube(pos, new Vector3(1f, 0, 1f));
		}
	}
}