using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    // The color after the ring has been touched
    [SerializeField] private Color scoredColor;
    // The color before the ring has been touched
    [SerializeField] private Color unscoredColor;
    // The sound that plays when you pick up a ring
    [SerializeField] private AudioSource pickupSound;

    private Component[] lights;

    // Called when the ring is hit by the player.
    public void scoreRing()
    {
        setLightColor(scoredColor);
        pickupSound.Play();
    }
    
    // Resets the ring to standard
    public void resetRing()
    {
        setLightColor(unscoredColor);
    }

    // Set's the ring's lights to the specified color
    private void setLightColor(Color color)
    {
        foreach (Light light in lights)
            light.color = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        lights = GetComponentsInChildren<Light>();
    }
}
