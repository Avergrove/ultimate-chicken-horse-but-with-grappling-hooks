using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// A tile that is scripted to have something happen when interacted with, e.g. when colliding with it or when grappling it.
/// Contains utility functions for evaluating what happens.
/// </summary>
[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(Rigidbody2D))]
public class ScriptedTile : MonoBehaviour
{
    protected Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Retrieves the collided tile when given a location.
    /// </summary>
    /// <param name="interactionPoint">The location in which an event happens i.e. contact point</param>
    protected Vector3Int GetCollidedTilePositionInGrid(Vector2 interactionPoint)
    {
        this.tilemap = GetComponent<Tilemap>();
        Grid grid = tilemap.layoutGrid;
        return grid.WorldToCell(interactionPoint);
    }

    protected Vector2 GetWorldPositionOfCollidedTile(Vector3Int tilePos)
    {
        return tilemap.GetCellCenterWorld(tilePos);
    }
}
