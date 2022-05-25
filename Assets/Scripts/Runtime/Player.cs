using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what a player can do.
/// </summary>
public class Player : MonoBehaviour
{

    Rigidbody2D rgbd;
    Vector2 aimingDirection;

    // Inventory
    public IItem heldItem;

    // Movement stats
    public float maxGroundSpeed;
    public float xPercentSpeedDecay;
    public float jumpInitialSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // A player can....
    // Dev Notes: Using velocity will cause collision into the tiles, so be careful!
    // You need to use force instead and clamp the velocity
    public void Move(Vector2 input)
    {

        if(input.x > 0)
        {
            rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
        }

        else if(input.x < 0)
        {
            rgbd.velocity = new Vector2(maxGroundSpeed * input.x, rgbd.velocity.y);
        }

        else
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x * xPercentSpeedDecay, rgbd.velocity.y);
        }
    }

    public void Jump()
    {
        rgbd.velocity = new Vector2(rgbd.velocity.x, jumpInitialSpeed);
    }
}
