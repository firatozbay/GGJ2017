using UnityEngine;
using System.Collections;

public class WaveText : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void WaveAnimationEvent()
    {
        GameManager.Instance.AnimationEnd();
    }
}
