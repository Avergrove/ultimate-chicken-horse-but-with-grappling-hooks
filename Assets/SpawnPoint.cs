using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A spawn point object. A spawn point is configured for a player when they enter the vicinity
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Tags.Player.ToString()))
        {
            PlayerAction playerAction = collision.GetComponent<PlayerAction>();
            playerAction.spawnPoint = this.gameObject;
        }
    }
}
