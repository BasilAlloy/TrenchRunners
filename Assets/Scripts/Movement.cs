using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	[SerializeField] private float speed = 25f;

	// The initial forward velocity of the ship
	[SerializeField] private float initialThrust = 25f;	
	// How much the ship speeds up each tick
	[SerializeField] private float acceleration = 0.005f;
	// How fast the ship can roll
	[SerializeField] private float RollSpeed = 64;
	[SerializeField] private AudioSource crashSound;
	// Direction the ship is moving in
	private Vector3 direction = Vector3.zero;
	// Records the start position of the ship so we can reset to it
	private Vector3 startPosition;

	// List of all the rings that have been collected this run.
	private List<Ring> collectedRings = new List<Ring>();
	private float rollSpeed;
	private float currentAcceleration;
	
	private void Start() {
        startPosition = gameObject.transform.position;
		rollSpeed = 2 * Mathf.PI/360 * RollSpeed;
		// Initialize game state
		reset();
    }

	// Update is called once per frame
	void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		direction = new Vector3(horizontal, vertical, 0f).normalized;

		// Move ship towards the mouse when lmb is held down
		// if (Input.GetMouseButton(0))
		// {
		// 	direction = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0f).normalized;
		// }

		// Rotate the ship left if lmb is held down, rotate right if rmb is held down.
		var rotateDir = 0;
		if (Input.GetMouseButton(0))
		{	rotateDir += 1;
		} 
		if (Input.GetMouseButton(1))
		{	rotateDir -= 1;
		}

		Vector3 moveVector = transform.TransformDirection(direction) * speed;
		rb.velocity = new Vector3(moveVector.x, moveVector.y, rb.velocity.z + currentAcceleration);
		rb.angularVelocity = new Vector3(0,0, rotateDir * rollSpeed);
	}

	// Pause and resume the ship when the game is paused.
	public void Pause()
	{
		currentAcceleration = 0f;
	}

	public void Resume() 
	{
		currentAcceleration = acceleration;
	}

	// Whenever the ship collides with an object tagged with killship, reset the ship.
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collider>().tag == "KillShip")
        {
			crashSound.Play();
            reset();
        } else if (other.GetComponent<Collider>().tag == "Ring")
		{
			// Score the ring
			Ring ring = other.GetComponent<Ring>();
			if (ring != null)
			{	ring.scoreRing();
				collectedRings.Add(ring);
			}
			ScoreManager.instance.AddPoints(100);
		}
    }

	// Reset the ship's speed and position to the start of the trench
	private void reset() {
        rb.velocity = new Vector3(0, 0, initialThrust);
		gameObject.transform.position = startPosition;
		gameObject.transform.rotation = Quaternion.identity;
		// Reset the current score
		ScoreManager.instance.ResetScore();
		// Reset all the scored rings
		foreach(Ring ring in collectedRings)
		{
			ring.resetRing();
		}
    }

}
