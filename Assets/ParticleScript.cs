using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Particle";

    }

    // Update is called once per frame
    void Update () {
	
	}
}
