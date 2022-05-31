using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEditor : MonoBehaviour
{
    public SpriteRenderer placeholderSr;
    public GameObject toSpawn;
    public GameObject interactablesTilemap;

    public Grid deployablePlatformGrid;

    // Start is called before the first frame update
    void Start()
    {
        placeholderSr.sprite = toSpawn.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Moves cursor to position
    /// </summary>
    public void MoveCursor(Vector2 cursorPosition)
    {
        this.transform.position = (Vector2) Camera.main.ScreenToWorldPoint(cursorPosition);
        // First, we need to check what cell is the cursor in



        // Then, we update the cursor to show whether the position is occupied by another object, and updatr the color based on whether it can be deployed.
    }

    public void DeployPlatform(Vector2 mousePosition)
    {
        GameObject spawnedObject = GameObject.Instantiate(toSpawn, interactablesTilemap.transform);
        spawnedObject.transform.position = this.transform.position;
    }
}
