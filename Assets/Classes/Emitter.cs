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
			GameObject newobj = (GameObject)Instantiate(wavePrefab, transform.position, Quaternion.identity);
            newobj.transform.parent = gameObject.transform;
			timer = EMISSION_RATE;
        }
    }
}
