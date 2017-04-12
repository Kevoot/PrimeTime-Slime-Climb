using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody2D rb;
    public SlimeFace slimeFace;
    public SpriteRenderer sRenderer;
    public GameObject CongratsBanner;
    public SlimeCamera playerCamera;
    public static bool CanMoveVertical;
    public float MovementSpeed;
    public float MaxXVelocity, MaxYVelocity;
    public int ForceMultiplier;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            Vector3 movement;
            if (CanMoveVertical)
            {
                movement = new Vector3(Input.acceleration.x, Input.acceleration.y, 0.0f);
            }
            else
            {
                movement = new Vector3(Input.acceleration.x,  0.0f, 0.0f);
            }
        }
        else
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * MaxXVelocity;
            var y = Input.GetAxis("Vertical") + Time.deltaTime * MaxYVelocity;
            if (CanMoveVertical)
            {
                if((rb.velocity.x >= 0 && rb.velocity.x < MaxXVelocity) || (rb.velocity.x < 0 && rb.velocity.x > (MaxXVelocity * -1)) &&
                    (rb.velocity.y >= 0 && rb.velocity.y < MaxYVelocity) || (rb.velocity.y < 0 && rb.velocity.y > (MaxYVelocity * -1)))
                {
                    rb.AddForce(new Vector2(x, y) * ForceMultiplier);
                }
            }
            else
            {
                if (((rb.velocity.x >= 0 && rb.velocity.x < MaxXVelocity) || rb.velocity.x < 0 && rb.velocity.x > (MaxXVelocity * -1)))
                {
                    rb.AddForce(new Vector2(x, 0) * ForceMultiplier);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision entered");
        if(other.tag == "EventTrigger")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 75);
            playerCamera.LockCamera();
            CongratsBanner.transform.position = new Vector3(CongratsBanner.transform.position.x, CongratsBanner.transform.position.y, 99);

        }

    }
}
