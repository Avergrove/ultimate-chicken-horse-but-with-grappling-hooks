using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public Canvas pauseCanvas;
    private bool finished;

    private Stack<GameObject> uiStack;

    // Start is called before the first frame update
    void Start()
    {
        uiStack = new Stack<GameObject>();
    }

    public void ShowUI()
    {
        pauseCanvas.gameObject.SetActive(true);
        uiStack.Push(pauseCanvas.gameObject);
    }

    public void HideUI()
    {
        pauseCanvas.enabled = false;
    }

    /// <summary>
    /// Requests the UI panel to go back one stage, and close if at main menu.
    /// </summary>
    public void Back()
    {
        if (uiStack.Count > 0)
        {
            GameObject stackTop = uiStack.Pop();
            stackTop.SetActive(false);
        }
    }

    public bool HasActivePanels()
    {
        return uiStack.Count > 0;
    }
}
