using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private PlatformFactory platformFactory;
    List<Vector2> platformPoints;



    // Start is called before the first frame update
    void Start()
    {
        platformFactory = GetComponentInChildren<PlatformFactory>();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
