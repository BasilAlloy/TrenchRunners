using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrenchGenerator : MonoBehaviour
{
    public GameObject ship;
    public GameObject blocker;
    public GameObject panel;

    // The seed used for the trench generation
    public int trenchSeed;

    // The diameter of the trench
    public float trenchScale;

    // How much space to put between blockers
    public float blockerSpacing;

    // How far out to spawn the blockers
    public float trenchLength;

    // How much room the ship has before the blockers start
    public float leadSpace;

    public Vector3 trenchStart = new Vector3(0,0,0);

    public int panels = 12;

    private float lastBlockerZ;

    void spawnTrenchSection(float z) {
        var sectionCenter = trenchStart + new Vector3(0,0,z);
        // Spawn 12 panels to form the walls of the trench
        for (float r = 0f; r < 2*Mathf.PI; r += 2*Mathf.PI/panels) {
            var panelPosition = sectionCenter + new Vector3(Mathf.Cos(r) * trenchScale, Mathf.Sin(r) * trenchScale, 0);
            var inst = Instantiate(panel, panelPosition, Quaternion.Euler(-r * Mathf.Rad2Deg, 90f, 0));
            inst.transform.parent = gameObject.transform;
        }

        spawnBlocker(z);
    }

    void spawnBlocker(float z)
    {
        var position = trenchStart + new Vector3(Random.Range(-trenchScale, trenchScale), Random.Range(-trenchScale, trenchScale), z);
        var rotation = Quaternion.Euler(0, 0, Random.Range(0,360));
        var inst = Instantiate(blocker, position, rotation);
        inst.transform.parent = gameObject.transform;
        
        lastBlockerZ = z;
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(trenchSeed);
        
        for (float z = leadSpace; z < trenchLength; z+=blockerSpacing)
        {
            spawnTrenchSection(z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lastBlockerZ + blockerSpacing - ship.transform.position.z < trenchLength) {
            for (float z = lastBlockerZ; z < ship.transform.position.z + trenchLength + blockerSpacing; z+=blockerSpacing) {
                spawnTrenchSection(z);
            }
        }
    }
}
