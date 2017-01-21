using UnityEngine;
using System.Collections;

public class MeteorGenerator : MonoBehaviour {
    public float TimeGap = 2; 
    public GameObject[] meteors;
    private float timer;
    private int cnt;
    // Use this for initialization
    void Start () {
        timer = TimeGap;
        cnt = 0;
	}

	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            //Generate Meteor
            cnt = (cnt + 1) % meteors.Length;
            Instantiate(meteors[cnt]);
            timer = TimeGap;
        }    
	}
}
