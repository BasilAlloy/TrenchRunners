using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{

	public CharacterController controller;

    // initial forward velocity of the ship
    public float speed = 25f;

    // How much the ship speeds up each tick
    public float acceleration = 0.005f;

    private float currentSpeed;
    private Vector3 startPosition;

    private void Start() {
        currentSpeed = speed;
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
	    controller.Move(new Vector3(0, 0, currentSpeed) * Time.deltaTime);
        currentSpeed += acceleration;
        
    }

    // Whenever the ship collides with an object tagged with killship, reset the ship.
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collider>().tag == "KillShip")
        {
            resetShip();
        }
    }

    private void resetShip() {
        currentSpeed = speed;
        Debug.Log("Reset");
    }
}
