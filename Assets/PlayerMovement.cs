using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rgbd;
    RopeSystem ropeSystem;

    // Grounded raycasts
    public ContactFilter2D groundedRaycastFilter;
    private RaycastHit2D[] hitResults;

    // Entity state
    public bool isGrounded;
    public bool isGrappled;

    // Movement stats
    public float maxGroundSpeed;
    [Tooltip("Speed lost in percent on every tick.")]
    public float xPercentSpeedDecay;
    public float xPercentAirSpeedDecay;
    public float jumpInitialSpeed;

    public float grappleJumpBoost;

    // Start is called before the first frame update
    void Start()
    {
        ropeSystem = GetComponent<RopeSystem>();
        isGrappled = false;
        isGrounded = false;

        hitResults = new RaycastHit2D[1];
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundedRaycastFilter, hitResults, 2) > 0;
    }

    public void Move(Vector2 input)
    {
        if (isGrappled)
        {
            
        }

        else
        {
            if (isGrounded)
            {
                if (input.x > 0)
                {
                    rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                }

                else if (input.x < 0)
                {
                    rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                }

                else
                {
                    rgbd.velocity = new Vector2(rgbd.velocity.x * xPercentSpeedDecay / 100, rgbd.velocity.y);
                }
            }

            else
            {
                if (input.x > 0)
                {
                    if (rgbd.velocity.x < maxGroundSpeed)
                    {
                        rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                    }
                }

                else if (input.x < 0)
                {
                    rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
                }

                else
                {
                    // Speed decays slower in the air
                    rgbd.velocity = new Vector2(rgbd.velocity.x * xPercentSpeedDecay / 100, rgbd.velocity.y);
                }
            }
        }
    }

    public void Jump()
    {
        if (!isGrappled)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpInitialSpeed);
        }

        else
        {
            this.ReleaseGrapplingHook(Vector2.zero);
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y + grappleJumpBoost);
        }
    }

    public void FireGrapplingHook(Vector2 cursorPosition)
    {
        isGrappled = ropeSystem.Fire(cursorPosition);
    }

    public void ReleaseGrapplingHook(Vector2 cursorPosition)
    {
        ropeSystem.DetachRope();
        isGrappled = false;
    }
}
