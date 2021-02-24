using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{
	Renderer rend;
    void Start()
    {
		rend = GetComponent<Renderer>();
    }

	private void OnMouseEnter() {
		rend.materials[0].color = Color.red;
	}

	private void OnMouseExit() {
		rend.material.color = Color.white;
	}
}
