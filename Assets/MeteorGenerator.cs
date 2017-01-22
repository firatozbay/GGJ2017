using UnityEngine;
using System.Collections;

public class MeteorGenerator : MonoBehaviour {
    public float[] timeGaps; 
    public GameObject[] meteors;
    private float[] timers;
    GameObject pool;
    // Use this for initialization
    void OnEnable()
    {
        pool = new GameObject("MeteorPool");
        pool.transform.parent = gameObject.transform;
    }
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
                newObj.transform.parent = pool.transform;
                newObj.SetActive(true);
                timers[i] = timeGaps[i];
            }
        }
	}
    void OnDisable()
    {
        Destroy(pool);
    }
}
