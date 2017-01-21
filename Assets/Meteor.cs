using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    private enum AnimState { Idle = 0, Pump = 1, Squeeze =2 }
    private AnimState animState;
    private Transform EmmisionCenter;

    private Transform meteorCenter;

    private Animator animator;

    public float rotateSpeed;
    public float gravity;
    public int hitpoints;

    private float SQUEEZE_TIME = 0.1f;
    private float PUMP_TIME = 0.1f;

    private float timer;

    // Use this for initialization
    void Start()
    {
        animState = AnimState.Idle;
        hitpoints = 2;
        EmmisionCenter = GameObject.FindWithTag("EmmisionCenter").transform;
        transform.parent.position = EmmisionCenter.position;
        meteorCenter = transform.parent;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        meteorCenter.Rotate(new Vector3(0, 0, -1*rotateSpeed));
        transform.Translate(new Vector3(-0.05f*gravity, 0, 0));
        if(animState == AnimState.Pump)
        {
            Pump();
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                timer = SQUEEZE_TIME;
                animState = AnimState.Squeeze;
            }
        }else if(animState == AnimState.Squeeze)
        {
            Squeeze();
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                animState = AnimState.Idle;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Break();
        }
    }
    public void OnBullet(int damage)
    {
        hitpoints-= damage;
        if (hitpoints <= 0)
        {
            OnZeroHealth();
        }
        else
        {
            animState = AnimState.Pump;
            timer = PUMP_TIME;
        }
    }
    void Pump()
    {
        transform.localScale = new Vector3(transform.localScale.x * 1.05f,
                                            transform.localScale.y * 1.05f,
                                            transform.localScale.z * 1.05f);
    }
    void Squeeze()
    {
        transform.localScale = new Vector3(transform.localScale.x * 0.95f,
                                            transform.localScale.y * 0.95f,
                                            transform.localScale.z * 0.95f);
    }
    void OnZeroHealth()
    {
        animator.SetTrigger("Explode");
        Debug.Log("Explode");
    }
    void DestroyEvent()
    {
        Destroy(transform.parent.gameObject);
    }
}
