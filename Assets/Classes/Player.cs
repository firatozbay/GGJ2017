using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player Instance;
    private Animator animator;
    public Transform EmmisionCenter;


    public GameObject bulletPrefab;
    private GameObject bullet;
    private float timer;
    private float FIRE_TIMER = 0.25f;
    private bool holdgun;
    public float speed = 1;
    private Vector2 initialPosition;
	private LensFlare flare;

	private int _color;
	public int color {
		get {
			return _color;
		}
		set {
			_color = value;
			flare.color = color < 0 ? Color.black : Wave.COLORS[color];
		}
	}

    public bool isDead;

    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		flare = GetComponent<LensFlare>();
        isDead = false;
        animator = GetComponent<Animator>();
        transform.parent.position = EmmisionCenter.position;
        initialPosition = transform.position;
        
        timer = 0;
        holdgun = false;

		color = -1;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isDead){
            if (holdgun)
            {
                if (timer < 0)
                {
                    holdgun = false;
                    animator.SetTrigger("StopFiring");
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("PressSword");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Todo add animation
                if (holdgun)
                {
                    bullet = (GameObject)Instantiate(bulletPrefab, transform.position + new Vector3(1, 0.2f, 0), Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(9f, 0);//ToDo Change Later to pointer
					bullet.GetComponent<Bullet>().color = color;
                }
                else
                {
                    animator.SetTrigger("PressFire");
                }
                timer = FIRE_TIMER;
            }

            if (Translated(transform.position))
            {
                Debug.Log("Game Over");
                GameManager.Instance.GameOver();
            }
            if (RotatedUp(transform.position))
            {
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(new Vector3(0, speed * -0.05f, 0));
                }
            }
            if (RotatedDown(transform.position))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(new Vector3(0, speed * 0.05f, 0));
                }
            }
            if (RotatedDown(transform.position) && RotatedUp(transform.position))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(new Vector3(speed * -0.05f, 0, 0));
                }
            }
            else
            {
                transform.Translate(new Vector3(speed * -0.05f, 0, 0), Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(speed * 0.05f, 0, 0));
			}
			animator.SetBool("Left", Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D));
			animator.SetBool("Right", !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D));
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
    public void SetInitialPosition()
    {
        transform.position = initialPosition;
        isDead = true;
    }
    public void Reactivate()
    {
        isDead = false;
    }

	void FixedUpdate() {
		color = -1;
	}
    public void SetDeathBool(bool value)
    {
        animator.SetBool("Dead", value);
    }
}
