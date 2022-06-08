using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	[SerializeField] private float speed = 25f;

	// The initial forward velocity of the ship
	[SerializeField] private float initialThrust = 25f;	
	// How much the ship speeds up each tick
	[SerializeField] private float acceleration = 0.005f;
	// Direction the ship is moving in
	private Vector3 direction = Vector3.zero;
	// Records the start position of the ship so we can reset to it
	private Vector3 startPosition;
	
	private void Start() {
        startPosition = gameObject.transform.position;

		// Initialize Ship state
		resetShip();
    }

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
		rb.velocity = new Vector3(moveVector.x, moveVector.y, rb.velocity.z + acceleration);
	}

	// Whenever the ship collides with an object tagged with killship, reset the ship.
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collider>().tag == "KillShip")
        {
            resetShip();
        }
    }

	// Reset the ship's speed and position to the start of the trench
	private void resetShip() {
        rb.velocity = new Vector3(0, 0, initialThrust);
		gameObject.transform.position = startPosition;
    }

}
