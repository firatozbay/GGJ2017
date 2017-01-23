using UnityEngine;
using System.Collections;

public class Weakness : MonoBehaviour {
	private int color;

	void Start() {
		color = GetComponentInParent<Wave>().color;
	}

	void OnTriggerStay2D(Collider2D col) {
		Player player = col.GetComponent<Player>();
		if(player) {
			player.color = color;
            Time.timeScale = 0.25f;
		}
	}
}
