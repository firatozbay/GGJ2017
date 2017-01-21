using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Transform emmisionCenter;

    private float rotateSpeed;
    private float gravity;
    public float maxTilt = 100;
    public float minTilt = -100;

    // Use this for initialization
    void Start()
    {
        emmisionCenter = GameObject.FindWithTag("EmmisionCenter").transform;
        transform.position = new Vector3(transform.position.x - (emmisionCenter.position.x-transform.parent.position.x),
                                            transform.position.y - (emmisionCenter.position.y - transform.parent.position.y),
                                            transform.position.z - (emmisionCenter.position.z - transform.parent.position.z));
        transform.parent.position = emmisionCenter.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    


}
