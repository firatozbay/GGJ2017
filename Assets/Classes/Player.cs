﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private Animator animator;
    public Transform EmmisionCenter;
    public CameraController camController;

    public GameObject bulletPrefab;
    private GameObject bullet;
    private float timer;
    private float FIRE_TIMER = 0.25f;
    private bool holdgun;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        transform.parent.position = EmmisionCenter.position;


        timer = 0;
        holdgun = false;
    }

    // Update is called once per frame
    void Update() {
        if (holdgun)
        {
            if (timer < 0) {
                holdgun = false;
                animator.SetTrigger("StopFiring");
            } else {
                timer -= Time.deltaTime;
            }
        } 
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            animator.SetTrigger("ButtonUp");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Todo add animation
            if (holdgun)
            {
                bullet = (GameObject)Instantiate(bulletPrefab, transform.position + new Vector3(1, 0.2f, 0), Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(9f, 0);//ToDo Change Later to pointer
            }else{
                animator.SetTrigger("PressFire");
            }
            timer = FIRE_TIMER;
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("PressLeft");
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-0.05f, 0, 0));
            }
        }
        else
        {
            transform.Translate(new Vector3(-0.05f, 0, 0),Space.World);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("PressRight");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0.05f, 0, 0));
        }

    }
    public void AnimationEnded()
    {
        holdgun = true;
        bullet = (GameObject)Instantiate(bulletPrefab, transform.position+new Vector3(1, 0.2f, 0), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(9f, 0);//ToDo Change Later to pointer
    }
    public bool Translated(Vector3 position)
    {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(position);
        if (screenPos.x < 0.05f)
        {
            return true;
        }
        return false;
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
    }
}
