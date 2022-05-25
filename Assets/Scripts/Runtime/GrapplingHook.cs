using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour, IItem
{
    GameObject owner; // Owner is required to get relevant transform information.
    Vector2 aimPosition; // Cursor position is required from the owner when firing.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Cursor position is regularly updated to keep the grappling hook fired in the correct direction.
        
    }

    // When the grappling hook is used, it will get the owner's position and get the cursor's position, and fire at it.
    public void Use()
    {

    }
}
