using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableBackground : MonoBehaviour
{
    [Tooltip("The number of units to move before the texture repeats itself once for the furthermost prop")]
    public float baseUnitsMovedPerOffset;
    public float parallaxStrength;
    public Camera followingCamera;

    private Vector2 initialPosition;
    private Dictionary<GameObject, Renderer> rendererCache;
    private List<GameObject> objectLayers;

    // Start is called before the first frame update
    void Start()
    {
        // Cache up each components objects
        this.objectLayers = new List<GameObject>();
        this.rendererCache = new Dictionary<GameObject, Renderer>();
        foreach (Transform child in transform)
        {
            // NOTE: This assumes the children are returned in editor order!
            objectLayers.Add(child.gameObject);
            rendererCache.Add(child.gameObject, child.GetComponent<Renderer>());
        }

        // Attach to the following camera so it follows suit
        this.transform.parent = followingCamera.transform;
        this.initialPosition = followingCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int layerCount = 0;
        float parallaxMultiplier = 0;
        foreach (GameObject layer in objectLayers)
        {
            layerCount++;
            Material mat = rendererCache[layer].material;
            parallaxMultiplier = parallaxMultiplier + (Mathf.Pow(parallaxStrength, 1/1 + layerCount));

            Vector2 mainTextureOffset = mat.mainTextureOffset;
            Vector2 textureOffset = new Vector2((followingCamera.transform.position.x - initialPosition.x) * parallaxMultiplier / baseUnitsMovedPerOffset, mainTextureOffset.y);
            rendererCache[layer].material.mainTextureOffset = textureOffset;

        }

    }
}
