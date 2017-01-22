using UnityEngine;
using System.Collections;

public class MeteorGenerator : MonoBehaviour {
    public float[] timeGaps; 
    public GameObject[] meteors;
    private float[] timers;

    // Use this for initialization
    void Start () {
        timers = new float[meteors.Length];
        for (int i = 0; i < meteors.Length; i++)
        {
            timers[i] = timeGaps[i];
        }

	}

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < meteors.Length; i++)
        {
            timers[i] -= Time.deltaTime;

            if (timers[i] < 0)
            {
                GameObject newObj = (GameObject)Instantiate(meteors[i]);
                newObj.transform.parent = gameObject.transform;
                timers[i] = timeGaps[i];
            }
        }
	}
}
