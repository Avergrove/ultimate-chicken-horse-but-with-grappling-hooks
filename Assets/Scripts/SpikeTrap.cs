using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The spike trap is a platform type that kills the player if they move in the direction of the box collider
/// </summary>
public class SpikeTrap : ScriptedTile
{

    private AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kill the player if they are moving towards the direction of the collission
        // 1) Is it player that is touching?
        // 2) Is the player moving *towards* the spike?
        // 3) KILL

        if (collision.gameObject.CompareTag(Constants.tags[Constants.Tags.Player]))
        {
            // Rigidbody2D rgbd;
            PlayerAction playerMovement = collision.gameObject.GetComponent<PlayerAction>();
            this.Kill(playerMovement);
            /* TODO : Commented out due to edge case problem of identifying collided tiles.
            if (collision.gameObject.TryGetComponent<Rigidbody2D>(out rgbd))
            {

                Vector2 contactPoint = collision.GetContact(0).point;
                Vector3Int tilePos = this.GetCollidedTilePositionInGrid(contactPoint);
                Vector2 center = tilemap.GetCellCenterWorld(tilePos);

                bool right = contactPoint.x > center.x;
                bool top = contactPoint.y > center.y;

                if(top && rgbd.velocity.y < 0)
                {
                    this.Kill(playerMovement);
                }

                else if(!top && rgbd.velocity.y > 0)
                {
                    this.Kill(playerMovement);
                }

                else if (right && rgbd.velocity.x < 0)
                {
                    this.Kill(playerMovement);
                }

                else if(!right && rgbd.velocity.y > 0)
                {
                    this.Kill(playerMovement);
                }

            }
                */
        }
    }

    private void Kill(PlayerAction target)
    {
        target.Die(this.transform.position);
        aSource.Play();
    }

}
