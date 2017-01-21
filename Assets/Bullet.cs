using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float TIME_LEFT = 5;
    private float timer;
    public int damage;
	// Use this for initialization
	void Start () {
        timer = TIME_LEFT;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Meteor")
        {
            //Points
            col.gameObject.GetComponent<Meteor>().OnBullet(damage);
            Destroy(gameObject);
        }
    }
}
