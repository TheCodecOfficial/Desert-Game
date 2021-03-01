using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
	public Transform selectionOverlay;

	// REALLY TEMPORARY
	public int selectedType;

	public float ACTION_TIME = 1f;
	public float currentTileTime;

	public Slider acionTimeSlider;

	Vector2Int mousePos;

	private void Start() {
		mousePos = Vector2Int.zero;
	}

	void Update()
    {
		selectionOverlay.gameObject.SetActive(false);
		if (ScreenToWorld.MouseOverGround()) {
			DisplayOverlay();

			if (Input.GetMouseButton(0)) {
				Vector2Int newMousePos = ScreenToWorld.GetMousePoint();

				if (newMousePos != mousePos) currentTileTime = 0;
				mousePos = newMousePos;

				IncreaseActionTime();

				int mouseOverType = WorldData.GetTile(mousePos.x, mousePos.y).type;
				switch (mouseOverType) {
					case 0:
						// Mouse over sand / empty tile
						if (selectedType == 1) Debug.Log("plant cactus");
						if (selectedType == 2) Debug.Log("place building");
						break;
					case 1:
						// Mouse over cactus
						if (selectedType == 0) HarvestCactus();
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
	
	void HarvestCactus() {
		if (TimeOver()) {
			WorldData.UpdateTile(mousePos.x, mousePos.y, 0);
		}
	}

	void IncreaseActionTime() {
		currentTileTime += Time.deltaTime;
		if (TimeOver()) currentTileTime = 0;
		acionTimeSlider.value = currentTileTime / ACTION_TIME;
	}

	bool TimeOver() {
		return (currentTileTime >= ACTION_TIME);
	}

	void DisplayOverlay() {
		Vector2 mousePos = ScreenToWorld.GetMousePoint();
		Vector3 pos = new Vector3(mousePos.x, 0.01f, mousePos.y);
		selectionOverlay.position = pos;
		selectionOverlay.gameObject.SetActive(true);
	}
}