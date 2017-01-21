using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {
	private const float EMISSION_RATE = 3;

	public GameObject wavePrefab;

    private float timer;
	
	void Start () {
		timer = 0;
	}
	
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			Instantiate(wavePrefab, transform.position, Quaternion.identity);
			timer = EMISSION_RATE;
        }
    }
}
