﻿using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
    public float speed;
    public float x;
    public float y;
    public float z;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(speed*x,speed*y, speed*z));
	}
}
