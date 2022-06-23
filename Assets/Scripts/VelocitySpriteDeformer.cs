using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deforms a gameobject's sprite as scaled to their current velocity to improve springiness of animation.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class VelocitySpriteDeformer : MonoBehaviour
{

    public float xMaxDeform;
    public float xDeformMinSpeed;
    public float xDeformMaxSpeed;

    public float yMaxDeform;
    public float yDeformMinSpeed;
    public float yDeformMaxSpeed;

    private Player player;
    private Rigidbody2D rgbd;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GetComponent<Player>();
        this.rgbd = GetComponent<Rigidbody2D>();
        this.sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedX = Mathf.Abs(rgbd.velocity.x);
        float speedY = Mathf.Abs(rgbd.velocity.y);

        if (speedX > xDeformMinSpeed && !player.IsGrounded)
        {
            float clampedXSpeed = Mathf.Clamp(speedX, xDeformMinSpeed, xDeformMaxSpeed);
            float xSpeedInvLerp = Mathf.InverseLerp(xDeformMinSpeed, xDeformMaxSpeed, clampedXSpeed);

            sr.size = new Vector2(sr.size.x, Mathf.SmoothStep(1, yMaxDeform, xSpeedInvLerp));
        }

        else
        {
            sr.size = new Vector2(sr.size.x, 1);
        }

        if(speedY > yDeformMinSpeed)
        {
            float clampedYSpeed = Mathf.Clamp(speedY, yDeformMinSpeed, yDeformMaxSpeed);
            float ySpeedInvLerp = Mathf.InverseLerp(yDeformMinSpeed, yDeformMaxSpeed, clampedYSpeed);

            sr.size = new Vector2(Mathf.SmoothStep(1, xMaxDeform, ySpeedInvLerp), sr.size.y);
        }

        else
        {
            sr.size = new Vector2(1, sr.size.y);
        }
    }
}
