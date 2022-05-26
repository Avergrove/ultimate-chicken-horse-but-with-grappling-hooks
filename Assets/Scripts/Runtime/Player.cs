using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what a player can do.
/// </summary>
public class Player : MonoBehaviour
{

    Rigidbody2D rgbd;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
