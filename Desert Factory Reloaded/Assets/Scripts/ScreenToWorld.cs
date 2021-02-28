using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorld
{

	/// <summary>method <c>draw</c> renders the point.</summary>
	public static Vector2Int GetMousePoint() {

		Camera cam = Camera.main;
		Vector3 camPos = cam.transform.position;
		Vector2 mousePos = Input.mousePosition;
		Vector3 pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1f));
		Vector3 dir = camPos - pos;

		float alpha = -camPos.y / dir.y;

		Vector3 adjustedDir = alpha * dir;
		Vector3 adjustedPos = adjustedDir + camPos;

		return new Vector2Int(Mathf.RoundToInt(adjustedPos.x), Mathf.RoundToInt(adjustedPos.z));
	}

	public static bool MouseOverGround() {

		Camera cam = Camera.main;
		Vector3 camPos = cam.transform.position;
		Vector2 mousePos = Input.mousePosition;
		Vector3 pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1f));
		Vector3 dir = camPos - pos;

		return (dir.y > 0f);
	}
}