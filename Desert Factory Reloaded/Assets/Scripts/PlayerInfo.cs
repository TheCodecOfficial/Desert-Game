using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

	static new Transform transform;

	public static bool canMove;

	public void Start() {
		transform = gameObject.transform;
	}

	public static Vector3 getPosition() {
		return transform.position;
	}
}