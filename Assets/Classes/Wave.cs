using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
	private const float TARGET_SIZE = 50;
	private const float SPEED = 5;

	private float currentSize;

	void Start () {
		currentSize = 0;
	}
	
	void Update () {
		currentSize += Time.deltaTime * 5;
		transform.localScale = new Vector3(currentSize, currentSize, currentSize);
		if (currentSize >= TARGET_SIZE)
			Destroy(gameObject);
	}
}
