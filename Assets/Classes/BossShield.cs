using UnityEngine;
using System.Collections;

public class BossShield : MonoBehaviour {
	private SpriteRenderer shield;
	private float shieldTimer;
	private int color;

	void Start() {
		shield = transform.GetChild(0).GetComponent<SpriteRenderer>();
		shieldTimer = 0;
	}

	void Update() {
		shieldTimer -= Time.deltaTime;
		if (shieldTimer <= 0) {
			int newColor;
			while ((newColor = (int)Random.Range(0, Wave.COLORS.Length)) == color) ;
			color = newColor;
			shield.color = Wave.COLORS[color];
			shieldTimer = Random.Range(3, 6);
		}
		transform.position = new Vector2(transform.position.x, Player.Instance.transform.position.y);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Bullet bul = col.GetComponent<Bullet>();
		if(bul) {
			if(bul.color != color) {
				Destroy(bul.gameObject);
			}
		}
	}
}
