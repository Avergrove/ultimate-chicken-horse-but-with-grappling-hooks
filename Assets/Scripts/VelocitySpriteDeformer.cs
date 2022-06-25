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

    private Vector2 baseSize;


    // Start is called before the first frame update
    void Start()
    {
        this.player = GetComponent<Player>();
        this.rgbd = GetComponent<Rigidbody2D>();
        this.sr = GetComponent<SpriteRenderer>();
        this.baseSize = sr.size;
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

            sr.size = new Vector2(this.baseSize.x, Mathf.SmoothStep(this.baseSize.y, this.baseSize.y * yMaxDeform, xSpeedInvLerp));
        }

        else
        {
            sr.size = new Vector2(this.baseSize.x, this.baseSize.y);
        }

        if(speedY > yDeformMinSpeed)
        {
            float clampedYSpeed = Mathf.Clamp(speedY, yDeformMinSpeed, yDeformMaxSpeed);
            float ySpeedInvLerp = Mathf.InverseLerp(yDeformMinSpeed, yDeformMaxSpeed, clampedYSpeed);

            sr.size = new Vector2(Mathf.SmoothStep(this.baseSize.x, this.baseSize.x * xMaxDeform, ySpeedInvLerp), this.baseSize.y);
        }

        else
        {
            sr.size = new Vector2(this.baseSize.x, this.baseSize.y);
        }
    }
}
