using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : MonoBehaviour {
	private const int MAX_HEALTH = 6;

	private Image healthBar;
	private int health;

	void Start() {
		health = MAX_HEALTH;
		healthBar = GetComponentInChildren<Image>();
		healthBar.fillAmount = (float)health / MAX_HEALTH;
	}

	void OnTriggerEnter2D(Collider2D col) {
		Bullet bul = col.GetComponent<Bullet>();
		if (bul) {
			health -= bul.damage;
			healthBar.fillAmount = Mathf.Max((float)health / MAX_HEALTH, 0);
			if(health <= 0) {
				GetComponent<Animator>().SetTrigger("Die");
			}
		}
	}
}
