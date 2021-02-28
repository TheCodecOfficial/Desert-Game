using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		move *= speed * Time.deltaTime;

		transform.Translate(move);
    }
}