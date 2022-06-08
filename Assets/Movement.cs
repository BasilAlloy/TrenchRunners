using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	[SerializeField] private float speed = 25f;
	private Vector3 direction = Vector3.zero;

	// Update is called once per frame
	void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		direction = new Vector3(horizontal, vertical, 0f).normalized;

		if (Input.GetMouseButton(0))
		{
			direction = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0f).normalized;
		}

		Vector3 moveVector = transform.TransformDirection(direction) * speed;
		rb.velocity = new Vector3(moveVector.x, moveVector.y, rb.velocity.z);
	}

	
}
