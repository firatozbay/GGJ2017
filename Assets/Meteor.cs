using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{

    private Transform EmmisionCenter;

    private Transform meteorCenter;

    public float rotateSpeed;
    public float gravity;

    // Use this for initialization
    void Start()
    {
        EmmisionCenter = GameObject.FindWithTag("EmmisionCenter").transform;
        transform.parent.position = EmmisionCenter.position;
        meteorCenter = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        meteorCenter.Rotate(new Vector3(0, 0, -1*rotateSpeed));
        transform.Translate(new Vector3(-0.05f*gravity, 0, 0));
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            Destroy(this);
        }else if(col.gameObject.tag == "Player")
        {
            Debug.Break();
        }
    }
}
