using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public Transform selectionOverlay;

	// REALLY TEMPORARY
	public int selectedType;

    void Update()
    {
		selectionOverlay.gameObject.SetActive(false);
		if (ScreenToWorld.MouseOverGround()) {
			DisplayOverlay();

			if (Input.GetMouseButton(0)) {
				Vector2Int mousePos = ScreenToWorld.GetMousePoint();
				int mouseOverType = WorldData.GetTile(mousePos.x, mousePos.y).type;

				switch (mouseOverType) {
					case 0:
						// Mouse over sand / empty tile
						if (selectedType == 1) Debug.Log("plant cactus");
						if (selectedType == 2) Debug.Log("place building");
						break;
					case 1:
						// Mouse over cactus
						if (selectedType == 0) Debug.Log("harvest cactus");
						if (selectedType == 3) Debug.Log("harvest cactus");
						break;
					case 2:
						// Mouse over machine
						if (selectedType == 0) Debug.Log("open building GUI");
						if (selectedType == 3) Debug.Log("destroy building");
						break;
				}
			}
		}
    }

	void DisplayOverlay() {
		Vector2 mousePos = ScreenToWorld.GetMousePoint();
		Vector3 pos = new Vector3(mousePos.x, 0.01f, mousePos.y);
		selectionOverlay.position = pos;
		selectionOverlay.gameObject.SetActive(true);
	}
}