using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEditor : MonoBehaviour
{
    public SpriteRenderer placeholderSr;
    public GameObject toSpawn;
    public GameObject interactablesTilemap;

    public Grid deployablePlatformGrid;

    private Vector2 selectedSnapPosition;
    private bool posIsDeployed;
    private List<GridObject> deployedGridObjects;

    // Start is called before the first frame update
    void Start()
    {
        placeholderSr.sprite = toSpawn.GetComponent<SpriteRenderer>().sprite;
        deployedGridObjects = new List<GridObject>();
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
        // First, we need to check what cell the cursor is in, and snap the sprite to it.
        // To do that, we need to snap to closest rounding 
        this.transform.position = SnapToCell(Camera.main.ScreenToWorldPoint(cursorPosition), deployablePlatformGrid);
        this.selectedSnapPosition = this.transform.position;

        // Then, we update the cursor to show whether the position is occupied by another object, and updatr the color based on whether it can be deployed.
        // TODO: Update cell position.
        this.UpdateCursorState();

    }

    /// <summary>
    /// Deploys the selected platform at the mouse position.
    /// </summary>
    /// <param name="mousePosition"></param>
    public void DeployPlatform()
    {
        if (!posIsDeployed)
        {
            GameObject objectReference = GameObject.Instantiate(toSpawn, interactablesTilemap.transform);
            GridObject gridObject = new GridObject(objectReference, selectedSnapPosition);

            objectReference.transform.position = selectedSnapPosition;
            deployedGridObjects.Add(gridObject);
        }
    }

    public void DeletePlatform()
    {
        List<GridObject> objectsOnCell = deployedGridObjects.FindAll(x => x.getPosition().Equals(selectedSnapPosition));

        if (objectsOnCell.Count != 0)
        {
            objectsOnCell.ForEach(x => {
                x.Destroy();
                deployedGridObjects.Remove(x);
            });
        }

        UpdateCursorState();

    }

    /// <summary>
    /// Snaps a position to the nearest cell on a grid.
    /// </summary>
    /// <param name="position">The position to snap</param>
    /// <param name="grid">The grid to snap to</param>
    /// <returns name="snappedPosition">The snapped position </returns>
    private Vector2 SnapToCell(Vector2 position, Grid grid)
    {
        float x = Mathf.Round(position.x / grid.cellSize.x) * grid.cellSize.x;
        float y = Mathf.Round(position.y / grid.cellSize.y) * grid.cellSize.y;

        return new Vector2(x, y);
    }

    /// <summary>
    /// Updates the sprite based on whether there already is an object on the position.
    /// </summary>
    private void UpdateCursorState()
    {
        List<GridObject> objectsOnCell = deployedGridObjects.FindAll(x => x.getPosition().Equals(selectedSnapPosition));

        if (objectsOnCell.Count != 0)
        {
            placeholderSr.color = Color.red;
            posIsDeployed = true;
        }

        else
        {
            placeholderSr.color = Color.white;
            posIsDeployed = false;
        }
    }

    /// <summary>
    /// A class used to keep track of the objects deployed on each position
    /// </summary>
    private class GridObject
    {
        private GameObject heldObject;
        private Vector2 position;

        public GridObject(GameObject heldObject, Vector2 position)
        {
            this.heldObject = heldObject;
            this.position = position;
        }

        public GameObject getHeldObject()
        {
            return heldObject;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        /// <summary>
        /// Destroys the grid object.
        /// </summary>
        public void Destroy()
        {
            GameObject.Destroy(this.heldObject);
        }
    }
}
