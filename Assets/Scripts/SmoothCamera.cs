using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows a camera to follow an object smoothly.
/// </summary>
public class SmoothCamera : MonoBehaviour
{

    // TODO: Offset should be based on player facing direction.
    // TODO: Offset should follow cursor direction
    // TODO: Move offset code from player to camera.

    public Transform followingObject;
    public float dampTime;
    private Vector2 offset;
    private Vector2 velocity = Vector2.zero;


    // Start is called before the first frame update
    protected void Start()
    {
        this.offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (transform)
        {
            Vector2 destination = (Vector2) followingObject.transform.position + offset;

            transform.position = Vector2.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    /// <summary>
    /// Configures how far away the camera is actually from the target
    /// </summary>
    /// <param name="offset"></param>
    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }

    public Vector2 GetOffset()
    {
        return this.offset;
    }

    /// <summary>
    /// Requests the camera to perform a screen shake
    /// </summary>
    public void ScreenShake()
    {

    }
}
