using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Quits the game. Goodbye, cruel world!
    /// </summary>
    public void ExitToDesktop()
    {
        Application.Quit();
    }
}
