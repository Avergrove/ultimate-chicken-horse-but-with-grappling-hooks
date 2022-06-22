using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The spike trap is a platform type that kills the player if they move in the direction of the box collider
/// </summary>
public class SpikeTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Kill the player if they are moving towards the direction of the collission
        // 1) Is it player that is touching?
        // 2) Is the player moving *towards* the spike?
        // 3) KILL

        if (collider.gameObject.CompareTag(Constants.tags[Constants.Tags.Player]))
        {
            Rigidbody2D rgbd;
            PlayerMovement playerMovement = collider.GetComponent<PlayerMovement>();
            if (collider.TryGetComponent<Rigidbody2D>(out rgbd))
            {
                // Evaluate collider position
                Vector2 closestPoint = collider.ClosestPoint(this.transform.position);
                Vector2 center = this.transform.position;

                bool right = closestPoint.x > center.x;
                bool top = closestPoint.y > center.y;

                if(top && rgbd.velocity.y < 0)
                {
                    playerMovement.Die(center);
                }

                else if(!top && rgbd.velocity.y > 0)
                {
                    playerMovement.Die(center);
                }

                if (right && rgbd.velocity.x < 0)
                {
                    playerMovement.Die(center);
                }

                else if(!right && rgbd.velocity.y > 0)
                {
                    playerMovement.Die(center);
                }
            }
        }
    }
}
