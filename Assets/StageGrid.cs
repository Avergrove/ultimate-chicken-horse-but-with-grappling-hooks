using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws the grid's settings on the game and editor.
/// </summary>


public class StageGrid : MonoBehaviour
{

    public GameObject gridTilePrefab;

    /// <summary>
    /// A child in the gameObject to set the parent to keep the Hierarchy clean.
    /// </summary>
    public GameObject gridTileHolder;
    private Grid grid;
    private Vector2 tileSize;

    public int rowCount;
    public int colCount;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        tileSize = new Vector2(grid.cellSize.x, grid.cellSize.y);
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        GameObject referenceTile = (GameObject) Instantiate(gridTilePrefab);
        for(int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                GameObject tile = (GameObject)Instantiate(referenceTile, gridTileHolder.transform);
                float posX = col * tileSize.x;
                float posY = row * tileSize.y;

                tile.transform.position = new Vector2(posX, posY);
            }
        }
    }



}
