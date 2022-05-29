using UnityEngine;
using UnityEngine.UI;

public class PauseMenuFunctions : MonoBehaviour
{

    public InputManager inputManager;
    public PlatformEditorController platformEditorControllable;
    public Text textPanel;
    private string panelPlaceholder;
    
    private IControllable prevControllable;

    private void Start()
    {
        panelPlaceholder = "Currently Controlling: {0}";
        textPanel.text = string.Format(panelPlaceholder, "Default");
    }

    // Pause Panel functions
    /// <summary>
    /// Swap controller mode between input mode and controllable mode.
    /// </summary>
    public void OnSwapInputManagerButtonClick()
    {
        if (prevControllable == null) {
            prevControllable = inputManager.GetControllable();
            inputManager.SetControllable(platformEditorControllable);
            platformEditorControllable.gameObject.SetActive(true);
            textPanel.text = string.Format(panelPlaceholder, platformEditorControllable.name);
        }
        else
        {
            inputManager.SetControllable(prevControllable);
            platformEditorControllable.gameObject.SetActive(false);
            textPanel.text = string.Format(panelPlaceholder, "Default");
            prevControllable = null;
        }
    }
}
