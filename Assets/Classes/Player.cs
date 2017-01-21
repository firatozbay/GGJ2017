using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform EmmisionCenter;
    public CameraController camController;
    private Transform playerCenter;
    private Transform camCenter;
    private Rigidbody centerBody;
    private Rigidbody camBody;
    public GameObject bulletPrefab;
    private GameObject bullet;

    // Use this for initialization
    void Start () {
        transform.parent.position = EmmisionCenter.position;
        playerCenter = transform.parent;
        camCenter = camController.transform.parent;
        centerBody = playerCenter.gameObject.GetComponent<Rigidbody>();
        camBody = camCenter.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Todo add animation
            bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(9f,0);//ToDo Change Later to pointer
        }

        if (Translated(transform.position))
        {
            Debug.Log("Game Over");
            Debug.Break();
        }
        if (RotatedUp(transform.position)) {
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3( 0, -0.05f, 0));
            }
        }
        if (RotatedDown(transform.position)) { 
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3( 0, 0.05f, 0));
            }
        }
        if (RotatedDown(transform.position) && RotatedUp(transform.position))
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-0.05f, 0, 0));
            }
        }
        else
        {
            transform.Translate(new Vector3(-0.05f, 0, 0),Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0.05f, 0, 0));
        }
    }
    public bool Translated(Vector3 position)
    {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(position);
        if (screenPos.x < 0.05f)
        {
            return true;
        }
        return false;
        Debug.Log(screenPos);
    }
    public bool RotatedDown(Vector3 position)
    {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(position);
        if (screenPos.y > 0.95f)
        {
            return false;
        }
        return true;
    }
    public bool RotatedUp(Vector3 position)
    {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(position);
        if (screenPos.y < 0.05f)
        {
            return false;
        }
        return true;
        Debug.Log(screenPos);
    }
}
